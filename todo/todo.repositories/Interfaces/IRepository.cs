using System;
using System.Collections.Generic;
using System.Text;
using todo.building.blocks;

namespace todo.repositories.Interfaces
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
