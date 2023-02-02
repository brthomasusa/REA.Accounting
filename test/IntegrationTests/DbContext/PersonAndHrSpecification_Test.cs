using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Extensions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.IntegrationTests.Base;

namespace REA.Accounting.IntegrationTests.DbContext
{
    public class PersonAndHrSpecification_Test : TestBase
    {
        [Fact]
        public async Task PersonByIDWithEmployeeSpecification_ReturnOnePersonWithEmployee_ShouldSucceed()
        {
            //SETUP

            int businessEntityID = 2;
            CancellationToken cancellationToken = default;

            //ATTEMPT
            PersonDataModel? person = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<PersonDataModel>().AsNoTracking(),
                    new PersonByIDWithEmployeeSpec(businessEntityID)
                ).FirstOrDefaultAsync(cancellationToken);

            //VERIFY
            Assert.Equal("Duffy", person!.LastName);
            Assert.Equal("245797967", person.Employee!.NationalIDNumber);
        }

        [Fact]
        public async Task PersonByLastNameWithEmployeeSpecification_OneLetterCriteria_ShouldSucceed()
        {
            //SETUP

            string lastNameFragment = "a";
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var people = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<PersonDataModel>(),
                    new PersonByLastNameWithEmployeeSpec(lastNameFragment)
                ).ToListAsync(cancellationToken);

            //VERIFY
            Assert.True(people.Any());
        }

        [Fact]
        public async Task ValidateEmployeeNameIsUniqueSpec_NameIsUnique_ShouldReturnNull()
        {
            //SETUP
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var person = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<PersonDataModel>(),
                    new ValidateEmployeeNameIsUniqueSpec("John", "Doe", "J")
                ).FirstOrDefaultAsync(cancellationToken);

            //VERIFY
            Assert.Null(person);
        }

        [Fact]
        public async Task ValidateEmployeeNameIsUniqueSpec_NameIsNotUnique_ShouldReturnNotNull()
        {
            //SETUP
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var person = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<PersonDataModel>(),
                    new ValidateEmployeeNameIsUniqueSpec("David", "Bradley", "M")
                ).FirstOrDefaultAsync(cancellationToken);

            //VERIFY
            Assert.NotNull(person);
        }

        [Fact]
        public async Task ValidateNationalIdNumberIsUniqueSpec_IDIsUnique_ShouldReturnNull()
        {
            //SETUP
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var employee = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<Employee>().AsNoTracking(),
                    new ValidateNationalIdNumberIsUniqueSpec("295847004")
                ).FirstOrDefaultAsync(cancellationToken);

            //VERIFY
            Assert.Null(employee);
        }

        [Fact]
        public async Task ValidateNationalIdNumberIsUniqueSpec_IDIsNotUnique_ShouldReturnNotNull()
        {
            //SETUP
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var employee = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<Employee>(),
                    new ValidateNationalIdNumberIsUniqueSpec("295847284")
                ).FirstOrDefaultAsync(cancellationToken);

            //VERIFY
            Assert.NotNull(employee);
        }

        [Fact]
        public async Task ValidateEmployeeEmailIsUnique_EmailIsUnique_ShouldReturnNull()
        {
            //SETUP
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var person = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<PersonDataModel>(),
                    new ValidateEmployeeEmailIsUniqueSpec(@"kerri0@adventure-works.com")
                ).FirstOrDefaultAsync(cancellationToken);

            //VERIFY
            Assert.Null(person);
        }

        [Fact]
        public async Task ValidateEmployeeEmailIsUnique_EmailIsNotUnique_ShouldReturnNotNull()
        {
            //SETUP
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var person = await
                SpecificationEvaluator.Default.GetQuery
                (
                    _dbContext.Set<PersonDataModel>(),
                    new ValidateEmployeeEmailIsUniqueSpec(@"terri0@adventure-works.com")
                ).FirstOrDefaultAsync(cancellationToken);

            //VERIFY
            Assert.NotNull(person);
        }
    }
}