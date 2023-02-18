using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using REA.Accounting.Infrastructure.Persistence.Interfaces;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class PersonHasLastNameSpecification : SpecificationBase<PersonDataModel>
    {
        private readonly string _lastName;

        public PersonHasLastNameSpecification(string lastname)
            => _lastName = lastname;

        public override Expression<Func<PersonDataModel, bool>> ToExpression()
            => person => EF.Functions.Collate(person.LastName!, "SQL_Latin1_General_CP1_CI_AS") == _lastName;
    }
}