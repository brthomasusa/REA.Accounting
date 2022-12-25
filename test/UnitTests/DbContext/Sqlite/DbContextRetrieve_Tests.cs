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
        public async Task Get_EmployeeAggregate_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();
            await context.SeedPersonData();

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
    }
}