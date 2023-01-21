using Ardalis.Specification;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Person
{
    public class PersonByLastNameWithEmployeeSpec : Specification<PersonDataModel>
    {
        public PersonByLastNameWithEmployeeSpec(string lastName)
        {
            Query.Where(person => string.IsNullOrEmpty(lastName) || person.LastName!.Contains(lastName))
                 .Include(person => person.Employee!)
                 .Include(person => person.EmailAddresses!)
                 .Include(person => person.Telephones!)
                 .Include(person => person.BusinessEntityAddresses!)
                 .Include(person => person.Employee!.DepartmentHistories)
                 .Include(person => person.Employee!.PayHistories);

            // AddOrderBy(person => person.LastName!);
        }
    }
}