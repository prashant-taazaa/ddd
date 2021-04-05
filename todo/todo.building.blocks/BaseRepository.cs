using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.domain;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.shared
{
    public class BaseRepository<T,RContext> : IRepository<T> where T : Aggregate
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;

        public BaseRepository(IDbContext<RContext> dbContext)
        {
            _dbContext = dbContext as DbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(Guid id)
        {
            var entity = GetByID(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
            }

        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetByID(Guid id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public async System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
                     
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
