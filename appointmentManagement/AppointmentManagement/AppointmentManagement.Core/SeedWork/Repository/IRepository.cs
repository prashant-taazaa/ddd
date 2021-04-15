using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.Helpers.Domain
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        TEntity FindById(Guid id);
        IEnumerable<TEntity> Find();
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }

    public interface IRepository
    {

    }
}
