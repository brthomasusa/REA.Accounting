using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces.HumanResources
{
    public interface IEmployeeReadRepository
    {
        Task<Result<GetEmployeeDetailByIdResponse>> GetEmployeeDetailsById(int employeeId);
        Task<Result<GetEmployeeDetailsByIdWithAllInfoResponse>> GetEmployeeDetailsByIdWithAllInfo(int employeeId);
        Task<Result<PagedList<GetEmployeeListItemsResponse>>> GetEmployeeListItemsSearchByLastName(string lastName, PagingParameters pagingParameters);
    }
}