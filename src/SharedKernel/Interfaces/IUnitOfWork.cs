
namespace REA.Accounting.SharedKernel.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        // Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
