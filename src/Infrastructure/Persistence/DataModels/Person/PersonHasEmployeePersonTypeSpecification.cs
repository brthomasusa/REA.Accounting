using System.Linq.Expressions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class PersonHasEmployeePersonTypeSpecification : SpecificationBase<PersonModel>
    {
        private readonly string _personType;

        public PersonHasEmployeePersonTypeSpecification(string personType)
            => _personType = personType;

        public override Expression<Func<PersonModel, bool>> ToExpression()
            => person => person.PersonType!.ToUpper() == _personType.ToUpper();
    }
}