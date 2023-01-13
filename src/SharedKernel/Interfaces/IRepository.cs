using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.SharedKernel.Interfaces
{
    public interface IRepository<T>
    {
        Task<OperationResult<T>> GetByIdAsync(int id, bool asNoTracking = false);
        Task<OperationResult<int>> InsertAsync(T entity);
        Task<OperationResult<bool>> Update(T entity);
        Task<OperationResult<bool>> Delete(T entity);
    }
}
