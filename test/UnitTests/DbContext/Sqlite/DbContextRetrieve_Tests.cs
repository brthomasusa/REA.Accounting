using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;
using TestSupport.Helpers;
using System.Linq;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Extensions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Specifications;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.DbContext.Sqlite
{
    public class DbContextRetrieve_Tests
    {
        [Fact]
        public void Get_ContactTypes_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();

            //ATTEMPT
            var contactTypes = context.ContactType!.ToList();
            int count = contactTypes.Count;

            //VERIFY
            Assert.Equal(20, count);
        }

        [Fact]
        public void Get_AddressTypes_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();

            //ATTEMPT
            var addressTypes = context.AddressType!.ToList();
            int count = addressTypes.Count;

            //VERIFY
            Assert.Equal(6, count);
        }

        [Fact]
        public void Get_PhoneNumberTypes_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();

            //ATTEMPT
            var PhoneNumberTypes = context.PhoneNumberType!.ToList();
            int count = PhoneNumberTypes.Count;

            //VERIFY
            Assert.Equal(3, count);
        }

        [Fact]
        public async Task Get_CountryRegions_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();

            //ATTEMPT
            var regions = context.CountryRegion!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(238, count);
        }

        [Fact]
        public async Task Get_SalesTerritory_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();

            //ATTEMPT
            var regions = context.SalesTerritory!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(10, count);
        }

        [Fact]
        public async Task Get_StateProvince_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();

            //ATTEMPT
            var regions = context.StateProvince!.ToList();
            int count = regions.Count;

            //VERIFY
            Assert.Equal(181, count);
        }

        [Fact]
        public async Task DbContext_Person_HR_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonAndHrData();

            //ATTEMPT
            var businessEntities = context.BusinessEntity!.ToList();
            var people = context.Person!.ToList();
            var addresses = context.Address!.ToList();
            var businessEntityAddresses = context.BusinessEntityAddress!.ToList();
            var emailAddresses = context.EmailAddress!.ToList();
            var telephones = context.PersonPhone!.ToList();
            var departments = context.Department!.ToList();
            var shifts = context.Shift!.ToList();
            var employees = context.Employee!.ToList();
            var departmentHistories = context.EmployeeDepartmentHistory!.ToList();
            var payHistories = context.EmployeePayHistory!.ToList();

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
            Assert.Equal(33, businessEntityCount);
            Assert.Equal(33, peopleCount);
            Assert.Equal(33, addressCount);
            Assert.Equal(33, businessEntityAddressCount);
            Assert.Equal(33, emailAddressCount);
            Assert.Equal(33, telephoneCount);
            Assert.Equal(16, departmentCount);
            Assert.Equal(3, shiftCount);
            Assert.Equal(33, employeeCount);
            Assert.Equal(35, departmentHistoryCount);
            Assert.Equal(37, payHistoryCount);
        }

        [Fact]
        public async Task PersonByIDWithEmployeeSpecification_ReturnOnePersonWithEmployee_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonAndHrData();

            int businessEntityID = 2;
            CancellationToken cancellationToken = default;

            //ATTEMPT
            PersonModel? person = await
                SpecificationEvaluator.Default.GetQuery
                (
                    context.Set<PersonModel>().AsNoTracking(),
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
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonAndHrData();

            string lastNameFragment = "a";
            CancellationToken cancellationToken = default;

            //ATTEMPT
            var people = await
                SpecificationEvaluator.Default.GetQuery
                (
                    context.Set<PersonModel>(),
                    new PersonByLastNameWithEmployeeSpec(lastNameFragment)
                ).ToListAsync(cancellationToken);

            //VERIFY
            Assert.True(people.Any());
        }

        [Fact]
        public async Task DbContextExtension_ValidateWithSingleSpecifications_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonAndHrData();

            var filters = new List<SpecificationBase<PersonModel>>()
            {
                new PersonHasLastNameSpecification("Hamilton")
            };

            // IQueryable<PersonModel>? people = null;

            //ATTEMPT
            // Use Specification for query
            var people = context.Person!.ApplyFilters<PersonModel>(filters);

            //VERIFY
            Assert.Equal("Hamilton", people!.ToList()[0].LastName);

            // Use Specification for validation
            Assert.True(new PersonHasLastNameSpecification("Hamilton").IsSatisfiedBy(people!.ToList()[0]));
        }

        [Fact]
        public async Task DbContextExtension_ValidateWithMultipleSpecifications_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonAndHrData();

            PersonModel? person = await context.Person!.FindAsync(2);

            var filters = new List<SpecificationBase<PersonModel>>()
            {
                new PersonHasLastNameSpecification("Duffy"),
                new PersonHasEmployeePersonTypeSpecification("EM")
            };

            //ATTEMPT
            // Use multiple specifications for validation
            var people = context.Person!.ApplyFilters<PersonModel>(filters);
            bool isValid = person!.SatisfiesFilters<PersonModel>(filters);

            //VERIFY
            Assert.True(isValid);
        }
    }
}