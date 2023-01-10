using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Person
{
    public class PersonByIDWithEmployeeOnlySpec : Specification<PersonModel>, ISingleResultSpecification
    {
        public PersonByIDWithEmployeeOnlySpec(int businessEntityID)
        {
            Query.Where(person => person.BusinessEntityID == businessEntityID)
                 .Include(person => person.Employee!);
        }
    }
}