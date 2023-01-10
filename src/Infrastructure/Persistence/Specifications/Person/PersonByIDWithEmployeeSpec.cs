using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Person
{
    public class PersonByIDWithEmployeeSpec : Specification<PersonModel>, ISingleResultSpecification
    {
        public PersonByIDWithEmployeeSpec(int businessEntityID)
        {
            Query.Where(person => person.BusinessEntityID == businessEntityID)
                 .Include(person => person.Employee!)
                 .Include(person => person.EmailAddresses!)
                 .Include(person => person.Telephones!)
                 .Include(person => person.BusinessEntityAddresses!)
                 .Include(person => person.Employee!.DepartmentHistories)
                 .Include(person => person.Employee!.PayHistories);
        }
    }
}