using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Extensions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.IntegrationTests.Base;

namespace REA.Accounting.IntegrationTests.DbContext
{
    public class DbContextRetrieve_Tests : TestBase
    {
        [Fact]
        public void Get_ContactTypes_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var contactTypes = _dbContext.ContactType!.ToList();
            int count = contactTypes.Count;

            //VERIFY
            Assert.Equal(20, count);
        }

        [Fact]
        public void Get_AddressTypes_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var addressTypes = _dbContext.AddressType!.ToList();
            int count = addressTypes.Count;

            //VERIFY
            Assert.Equal(6, count);
        }

        [Fact]
        public void Get_PhoneNumberTypes_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var PhoneNumberTypes = _dbContext.PhoneNumberType!.ToList();
            int count = PhoneNumberTypes.Count;

            //VERIFY
            Assert.Equal(3, count);
        }

        [Fact]
        public void Get_CountryRegions_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var regions = _dbContext.CountryRegion!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(238, count);
        }

        [Fact]
        public void Get_SalesTerritory_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var regions = _dbContext.SalesTerritory!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(10, count);
        }

        [Fact]
        public void Get_StateProvince_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var regions = _dbContext.StateProvince!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(181, count);
        }

        [Fact]
        public void DbContext_Person_HR_ShouldSucceed()
        {
            //SETUP

            //ATTEMPT
            var businessEntities = _dbContext.BusinessEntity!.ToList();
            var people = _dbContext.Person!.ToList();
            var addresses = _dbContext.Address!.ToList();
            var businessEntityAddresses = _dbContext.BusinessEntityAddress!.ToList();
            var emailAddresses = _dbContext.EmailAddress!.ToList();
            var telephones = _dbContext.PersonPhone!.ToList();
            var departments = _dbContext.Department!.ToList();
            var shifts = _dbContext.Shift!.ToList();
            var employees = _dbContext.Employee!.ToList();
            var departmentHistories = _dbContext.EmployeeDepartmentHistory!.ToList();
            var payHistories = _dbContext.EmployeePayHistory!.ToList();

            int businessEntityCount = businessEntities.Count;
            int peopleCount = people.Count;
            int addressCount = addresses.Count;
            int businessEntityAddressCount = businessEntityAddresses.Count;
            int emailAddressCount = emailAddresses.Count;
            int telephoneCount = telephones.Count;
            int departmentCount = departments.Count;
            int shiftCount = shifts.Count;
            int employeeCount = employees.Count;
            int departmentHistoryCount = departmentHistories.Count;
            int payHistoryCount = payHistories.Count;

            //VERIFY
            Assert.True(businessEntityCount > 0);
            Assert.True(peopleCount > 0);
            Assert.True(addressCount > 0);
            Assert.True(businessEntityAddressCount > 0);
            Assert.True(emailAddressCount > 0);
            Assert.True(telephoneCount > 0);
            Assert.True(departmentCount > 0);
            Assert.True(shiftCount > 0);
            Assert.True(employeeCount > 0);
            Assert.True(departmentHistoryCount > 0);
            Assert.True(payHistoryCount > 0);
        }

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
        public void DbContextExtension_ValidateWithSingleSpecifications_ShouldSucceed()
        {
            //SETUP

            var filters = new List<SpecificationBase<PersonDataModel>>()
            {
                new PersonHasLastNameSpecification("Hamilton")
            };

            // IQueryable<PersonModel>? people = null;

            //ATTEMPT
            // Use Specification for query
            var people = _dbContext.Person!.ApplyFilters<PersonDataModel>(filters);

            //VERIFY
            Assert.Equal("Hamilton", people!.ToList()[0].LastName);

            // Use Specification for validation
            Assert.True(new PersonHasLastNameSpecification("Hamilton").IsSatisfiedBy(people!.ToList()[0]));
        }

        [Fact]
        public async Task DbContextExtension_ValidateWithMultipleSpecifications_ShouldSucceed()
        {
            //SETUP

            PersonDataModel? person = await _dbContext.Person!.FindAsync(2);

            var filters = new List<SpecificationBase<PersonDataModel>>()
            {
                new PersonHasLastNameSpecification("Duffy"),
                new PersonHasEmployeePersonTypeSpecification("EM")
            };

            //ATTEMPT
            // Use multiple specifications for validation
            var people = _dbContext.Person!.ApplyFilters<PersonDataModel>(filters);
            bool isValid = person!.SatisfiesFilters<PersonDataModel>(filters);

            //VERIFY
            Assert.True(isValid);
        }
    }
}