using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Organizations
{
    public sealed class CompanyByIdSpec : Specification<Company>, ISingleResultSpecification
    {
        public CompanyByIdSpec(int companyID)
        {
            Query.Where(company => company.CompanyID == companyID);
        }
    }
}