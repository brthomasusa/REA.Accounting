using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces.HumanResources
{
    public interface IEmployeeReadRepository
    {
        Task<Result<GetEmployeeDetailByIdResponse>> GetEmployeeDetailsById(int employeeId);
        Task<Result<EmployeeDetailReadModel>> GetEmployeeDetailsByIdWithAllInfo(int employeeId);
        Task<Result<PagedList<EmployeeListItemReadModel>>> GetEmployeeListItemsSearchByLastName(string lastName, PagingParameters pagingParameters);
    }
}