using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources
{
    public sealed class ValidateEmployeeEmailIsUniqueSpec : Specification<PersonDataModel>, ISingleResultSpecification
    {
        public ValidateEmployeeEmailIsUniqueSpec(string emailAddress)
            => Query.Include(person => person.EmailAddresses!)
                    .Where(e => e.EmailAddresses.All(addr => addr.MailAddress == emailAddress));
    }
}