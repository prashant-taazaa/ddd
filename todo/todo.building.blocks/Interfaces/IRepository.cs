using System;
using System.Collections.Generic;

namespace todo.infrastructure.shared.Interfaces
{
    public interface IRepository<T>
    {
        T GetByID(Guid id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
