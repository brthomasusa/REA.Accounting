using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Specifications.Person
{
    public class PersonByIDWithEmployeeSpecification : Specification<PersonModel>
    {
        public PersonByIDWithEmployeeSpecification(int businessEntityID)
            : base(person => person.BusinessEntityID == businessEntityID)
        {
            AddInclude(person => person.Employee!);
        }
    }
}