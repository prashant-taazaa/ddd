using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.persistence
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public IUserRepository UserRepository { get; private set; }
        public ITaskRepository TaskRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            UserRepository = new UserRepository(_dbContext);
            TaskRepository = new TaskRepository(_dbContext);
        }


        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
