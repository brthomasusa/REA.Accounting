using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.Shared.Models.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces.HumanResources
{
    public interface IEmployeeReadRepository
    {
        Task<Result<EmployeeDetailReadModel>> GetEmployeeDetailsByIdWithAllInfo(int employeeId);
        Task<Result<PagedList<EmployeeListItemReadModel>>> GetEmployeeListItemsSearchByLastName(string lastName, PagingParameters pagingParameters);
    }
}