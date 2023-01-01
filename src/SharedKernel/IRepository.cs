using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.SharedKernel
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
        Task<OperationResult<IQueryable<T>>> GetAll();
        Task<OperationResult<T>> GetByIdAsync(int id);
        Task<OperationResult<bool>> InsertAsync(T entity);
        Task<OperationResult<bool>> Delete(T entity);
    }
}
