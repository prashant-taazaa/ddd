using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todo.building.blocks;
using todo.repositories.Interfaces;

namespace todo.repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Aggregate
    {
        private TodoDbContext _dbContext;
        public BaseRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Add(T entity)
        {
             _dbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(Guid id)
        {
            var entity = GetByID(id);

            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
            }

        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public virtual T GetByID(Guid id)
        {
            return _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
