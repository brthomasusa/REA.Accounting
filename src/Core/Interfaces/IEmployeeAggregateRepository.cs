using REA.Accounting.Core.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Interfaces
{
    public interface IEmployeeAggregateRepository
    {
        Task<OperationResult<Employee>> GetById(int empployeeID);

        Task<OperationResult<bool>> Create(Employee employee);
    }
}