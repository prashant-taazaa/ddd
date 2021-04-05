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
        //private readonly IDbContext<RContext> _dbContext;
        //public  DbContext _c1 { get; private set; }

        public UnitOfWork( IRepository<TRepository> repository)
        {
            _repository = repository;
            //_dbContext = dbContext;
            //_c1 = dbContext as DbContext;

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
