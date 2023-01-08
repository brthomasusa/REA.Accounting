using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.SharedKernel
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
        Task<OperationResult<T>> GetByIdAsync(int id, bool asNoTracking = false);
        Task<OperationResult<int>> InsertAsync(T entity);
        Task<OperationResult<bool>> Delete(T entity);
    }
}
