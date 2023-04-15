using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces.Organization
{
    public interface ICompanyReadRepository
    {
        Task<Result<GetCompanyDetailByIdResponse>> GetCompanyDetailsById(int companyId);
        Task<Result<GetCompanyCommandByIdResponse>> GetCompanyCommandById(int companyId);
        Task<Result<PagedList<GetCompanyDepartmentsResponse>>> GetCompanyDepartments(PagingParameters pagingParameters);
        Task<Result<PagedList<GetCompanyShiftsResponse>>> GetCompanyShifts(PagingParameters pagingParameters);
    }
}