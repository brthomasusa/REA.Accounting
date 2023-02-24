using REA.Accounting.Core.HumanResources;
using REA.Accounting.SharedKernel.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Interfaces
{
    public interface IEmployeeAggregateRepository : IRepository<Employee>
    {
        Task<Result<Employee>> GetEmployeeOnlyAsync(int empployeeID, bool asNoTracking = false);
        Task<Result> ValidatePersonNameIsUnique(int id, string fname, string lname, string? middleName, bool asNoTracking = true);
        Task<Result> ValidateNationalIdNumberIsUnique(int id, string nationalIdNumber, bool asNoTracking = true);
        Task<Result> ValidateEmployeeEmailIsUnique(int id, string emailAddres, bool asNoTracking = true);
        Task<Result> ValidateEmployeeExist(int id, bool asNoTracking = true);
    }
}