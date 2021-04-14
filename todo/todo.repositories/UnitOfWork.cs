using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.persistence
{
    public class UnitOfWork<TRepository> : IUnitOfWork<TRepository>, IAsyncDisposable 
        where TRepository : IRepository
    {
        private readonly IRepository<TRepository> _repository;

        public UnitOfWork( IRepository<TRepository> repository)
        {
            _repository = repository;

        }

        public async Task<int> CompleteAsync()
        {
           return await _repository.SaveChangesAsync();
        }


        public ValueTask DisposeAsync()
        {
           return _repository.DisposeAsync();
        }
    }
}
