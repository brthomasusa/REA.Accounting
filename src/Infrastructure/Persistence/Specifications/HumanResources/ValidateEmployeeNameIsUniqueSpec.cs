using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources
{
    public class ValidateEmployeeNameIsUniqueSpec : Specification<PersonDataModel>, ISingleResultSpecification
    {
        public ValidateEmployeeNameIsUniqueSpec
        (
            string firstName,
            string lastName,
            string? middleName
        )
            => Query.Where(person => person.FirstName!.ToUpper() == firstName.ToUpper() &&
                                     person.LastName!.ToUpper() == lastName.ToUpper() &&
                                     person.MiddleName!.ToUpper() == middleName!.ToUpper());
    }
}