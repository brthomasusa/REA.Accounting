using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources
{
    public class ValidateEmployeeNameIsUniqueSpec : Specification<PersonDataModel>, ISingleResultSpecification
    {
        private const string CI = "SQL_Latin1_General_CP1_CI_AS";

        public ValidateEmployeeNameIsUniqueSpec
        (
            string firstName,
            string lastName,
            string? middleName
        )
            => Query.Where(person => EF.Functions.Collate(person.FirstName!, CI) == firstName &&
                                     EF.Functions.Collate(person.LastName!, CI) == lastName &&
                                     EF.Functions.Collate(person.MiddleName!, CI) == middleName!);
    }
}