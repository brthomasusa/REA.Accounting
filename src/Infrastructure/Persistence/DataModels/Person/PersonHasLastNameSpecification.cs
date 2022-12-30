using System.Linq.Expressions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class PersonHasLastNameSpecification : SpecificationBase<PersonModel>
    {
        private readonly string _lastName;

        public PersonHasLastNameSpecification(string lastname)
            => _lastName = lastname;

        public override Expression<Func<PersonModel, bool>> ToExpression()
            => person => person.LastName!.ToUpper() == _lastName.ToUpper();
    }
}