using MediatR;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.SharedKernel;

namespace REA.Accounting.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _isDisposed;
        private readonly EfCoreContext _dbContext;

        public UnitOfWork(EfCoreContext ctx) => _dbContext = ctx;

        ~UnitOfWork() => Dispose(false);

        public async Task Commit() => await _dbContext.SaveChangesAsync();
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => await _dbContext.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
                _dbContext.Dispose();

            _isDisposed = true;
        }
    }
}