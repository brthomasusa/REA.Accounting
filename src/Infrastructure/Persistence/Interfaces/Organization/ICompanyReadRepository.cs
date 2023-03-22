using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces.Organization
{
    public interface ICompanyReadRepository
    {
        Task<Result<GetCompanyDetailByIdResponse>> GetCompanyDetailsById(int companyId);
        Task<Result<GetCompanyCommandByIdResponse>> GetCompanyCommandById(int companyId);
    }
}