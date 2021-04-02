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
    public class UnitOfWork<RContext> : IUnitOfWork<RContext>, IAsyncDisposable 
        where RContext: DbContext
    {
        private readonly IDbContext<RContext> _dbContext;
        private readonly DbContext _c1;

        public UnitOfWork(IDbContext<RContext> dbContext)
        {
            _dbContext = dbContext;
            _c1 = dbContext as DbContext;

        }

        //public async Task<int> CompleteAsync()
        //{
        //    return await _c1.SaveChangesAsync().ConfigureAwait(false);
        //}

        //public Task<int> CompleteAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public ValueTask DisposeAsync()
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<int> CompleteAsync()
        {
            return await _c1.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _c1.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ValueTask DisposeAsync()
        {
            return _c1.DisposeAsync();
        }
    }
}
