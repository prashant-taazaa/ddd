using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace todo.infrastructure.shared.Interfaces
{
    public interface IUnitOfWork<RContext> 
    {
        Task<int> CompleteAsync();

        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
