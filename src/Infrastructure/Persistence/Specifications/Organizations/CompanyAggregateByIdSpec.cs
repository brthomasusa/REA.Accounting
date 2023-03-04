using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Organizations
{
    public sealed class CompanyAggregateByIdSpec : Specification<Company>, ISingleResultSpecification
    {
        public CompanyAggregateByIdSpec(int companyID)
        {
            // Query.Where(company => company.CompanyID == companyID)
            //      .Include(company => company.)
            //      .Include(person => person.EmailAddresses!)
            //      .Include(person => person.Telephones!)
            //      .Include(person => person.BusinessEntityAddresses!)
            //         .ThenInclude(addr => addr.Address!)
            //      .Include(person => person.Employee!.DepartmentHistories)
            //      .Include(person => person.Employee!.PayHistories);            
        }
    }
}