using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Person
{
    public class PersonByLastNameWithEmployeeSpecification : Specification<PersonModel>
    {
        public PersonByLastNameWithEmployeeSpecification(string lastName)
            : base(person => string.IsNullOrEmpty(lastName) || person.LastName!.Contains(lastName))
        {
            AddInclude(person => person.Employee!);
            AddInclude(person => person.EmailAddresses!);
            AddInclude(person => person.Telephones!);
            AddInclude(person => person.Addresses!);

            AddInclude(person => person.Employee!.DepartmentHistories);
            AddInclude(person => person.Employee!.PayHistories);

            AddOrderBy(person => person.LastName!);
        }
    }
}