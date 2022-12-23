using REA.Accounting.UnitTests.TestHelpers;
using TestSupport.EfHelpers;
using TestSupport.Helpers;
using System.Linq;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.UnitTests.DbContext.Sqlite
{
    public class DbContextRetrieve_Tests
    {
        [Fact]
        public async Task TestSQLite_Setup_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedBusinessEntities();

            //ATTEMPT
            BusinessEntity? businessEntity = context.BusinessEntity!.Find(287);

            //VERIFY
            Assert.Equal(new Guid("d7d20616-c4c7-43c8-9fb8-7eba84aad8e1"), businessEntity!.RowGuid);
        }

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
        public async Task Get_Addresses_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonData();

            //ATTEMPT
            var addresses = context.Address!.ToList();
            int count = addresses.Count;

            //VERIFY
            Assert.Equal(33, count);
        }

        [Fact]
        public async Task Get_BusinessEntityAddresses_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonData();

            //ATTEMPT
            var businessEntityaddresses = context.BusinessEntityAddress!.ToList();
            int count = businessEntityaddresses.Count;

            //VERIFY
            Assert.Equal(33, count);
        }

        [Fact]
        public async Task Get_EmailAddresses_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedEmailAddressData();

            //ATTEMPT
            var emailAddresses = context.EmailAddress!.ToList();
            int count = emailAddresses.Count;

            //VERIFY
            Assert.Equal(33, count);
        }

        [Fact]
        public void Test_EmailRetrieval()
        {
            var emailAddresses = REA.Accounting.UnitTests.Data.LoadTestData.GetEmailAddressData();

            Assert.NotNull(emailAddresses);
        }

        [Fact]
        public async Task Get_EmployeeAggregate_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonData();

            //ATTEMPT
            var emailAddresses = context.EmailAddress!.ToList();
            int count = emailAddresses.Count;

            //VERIFY
            Assert.Equal(33, count);
        }
    }
}