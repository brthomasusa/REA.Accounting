using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources
{
    public sealed class ValidateEmployeeExistSpec : Specification<EmployeeDataModel>, ISingleResultSpecification
    {
        public ValidateEmployeeExistSpec(int employeeID)
            => Query.Where(employee => employee.BusinessEntityID == employeeID);
    }
}