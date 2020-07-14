using System;
using System.Threading;
using System.Threading.Tasks;

namespace Minos.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
