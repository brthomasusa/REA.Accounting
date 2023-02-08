using REA.Accounting.Core.HumanResources;
using REA.Accounting.SharedKernel.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Interfaces
{
    public interface IEmployeeAggregateRepository : IRepository<Employee>
    {
        Task<OperationResult<Employee>> GetEmployeeOnlyAsync(int empployeeID, bool asNoTracking = false);
        Task<OperationResult<bool>> ValidatePersonNameIsUnique(int id, string fname, string lname, string? middleName, bool asNoTracking = true);
        Task<OperationResult<bool>> ValidateNationalIdNumberIsUnique(int id, string nationalIdNumber, bool asNoTracking = true);
        Task<OperationResult<bool>> ValidateEmployeeEmailIsUnique(int id, string emailAddres, bool asNoTracking = true);
        Task<OperationResult<bool>> ValidateEmployeeExist(int id, bool asNoTracking = true);
    }
}