using REA.Accounting.Core.HumanResources;
using REA.Accounting.SharedKernel.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Interfaces
{
    public interface IEmployeeAggregateRepository : IRepository<Employee>
    {
        Task<OperationResult<Employee>> GetEmployeeOnlyAsync(int empployeeID, bool asNoTracking = false);
    }
}