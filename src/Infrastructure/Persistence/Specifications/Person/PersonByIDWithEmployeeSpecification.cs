using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Person
{
    public class PersonByIDWithEmployeeSpecification : Specification<PersonModel>
    {
        public PersonByIDWithEmployeeSpecification(int businessEntityID)
            : base(person => person.BusinessEntityID == businessEntityID)
        {
            AddInclude(person => person.Employee!);
            AddInclude(person => person.EmailAddresses!);
            AddInclude(person => person.Telephones!);
            AddInclude(person => person.Addresses!);

            AddInclude(person => person.Employee!.DepartmentHistories);
            AddInclude(person => person.Employee!.PayHistories);
        }
    }
}