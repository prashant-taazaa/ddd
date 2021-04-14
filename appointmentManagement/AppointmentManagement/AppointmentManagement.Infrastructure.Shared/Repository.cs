using AppointmentManagement.Core.Helpers.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Infrastructure.Shared
{
    public class Repository<T, RContext> : IRepository<T>
           where T: AggregateRoot 
           where RContext:DbContext
    {
        public readonly DbSet<T> _dbSet;
        public Repository(IDbContext<RContext> dbContext)
        {
            var context = dbContext as DbContext;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find()
        {
            throw new NotImplementedException();
        }

        public T FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
