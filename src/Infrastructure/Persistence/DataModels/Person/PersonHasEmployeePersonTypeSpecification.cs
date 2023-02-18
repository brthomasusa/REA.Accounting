using System.Linq.Expressions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class PersonHasEmployeePersonTypeSpecification : SpecificationBase<PersonDataModel>
    {
        private readonly string _personType;

        public PersonHasEmployeePersonTypeSpecification(string personType)
            => _personType = personType;

        public override Expression<Func<PersonDataModel, bool>> ToExpression()
            => person => string.Equals(person.PersonType, _personType, StringComparison.OrdinalIgnoreCase);
    }
}