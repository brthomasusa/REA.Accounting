using REA.Accounting.UnitTests.TestHelpers;
using TestSupport.EfHelpers;
using TestSupport.Helpers;
using System.Linq;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Extensions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;

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
        public async Task PersonHasLastNameSpecification_ReturnList_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonAndHrData();

            var filters = new List<Specification<PersonModel>>()
            {
                new PersonHasLastNameSpecification("Hamilton")
            };

            List<PersonModel>? people = null;

            //ATTEMPT
            filters.ForEach(item => people = context.Person!.Where(item.ToExpression()).ToList());

            //VERIFY
            Assert.True(people!.Any());
            Assert.Equal("Hamilton", people![0].LastName);
        }

        [Fact]
        public async Task PersonHasLastNameSpecification_ReturnSinglePersonModel_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonAndHrData();

            var specification = new PersonHasLastNameSpecification("Hamilton");

            //ATTEMPT
            PersonModel? person = context.Person!.Where(specification.ToExpression()).SingleOrDefault();

            //VERIFY
            Assert.Equal("Hamilton", person!.LastName);
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

            var filters = new List<Specification<PersonModel>>()
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

            var filters = new List<Specification<PersonModel>>()
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