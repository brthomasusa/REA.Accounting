using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources
{
    public sealed class ValidateNationalIdNumberIsUniqueSpec : Specification<Employee>, ISingleResultSpecification
    {
        public ValidateNationalIdNumberIsUniqueSpec(string nationalIdNumber)
            => Query.Where(employee => employee.NationalIDNumber == nationalIdNumber);
    }
}