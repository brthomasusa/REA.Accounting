using REA.Accounting.UnitTests.TestHelpers;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.UnitTests.DbContext.Sqlite
{
    public class PersonHrCreate_Tests
    {
        [Fact]
        public async Task Create_BusinessEntity_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();

            //ATTEMPT
            BusinessEntity businessEntity = new() { RowGuid = Guid.NewGuid(), ModifiedDate = DateTime.Now };
            context.BusinessEntity!.Add(businessEntity);
            await context.SaveChangesAsync();

            BusinessEntity? result = context.BusinessEntity!.FirstOrDefault();

            //VERIFY
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_BusinessEntityWithCompany_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            BusinessEntity entity = new()
            {
                BusinessEntityID = 1,
                Company = new()
                {
                    CompanyName = "AdventureWorks Cycles",
                    LegalName = "AdventureWorks Cycles LLC",
                    EIN = "12-3456789",
                    WebsiteUrl = "https:\\www.Adventureworkscycles.com"
                }
            };

            await context.BusinessEntity!.AddAsync(entity);
            await context.SaveChangesAsync();

            //ATTEMPT
            BusinessEntity? result = context.BusinessEntity!.FirstOrDefault();

            //VERIFY
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_BusinessEntityAddress_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();

            BusinessEntity entity = new()
            {
                BusinessEntityID = 1,
                BusinessEntityAddress = new()
                {
                    AddressID = 1,
                    AddressTypeID = 1
                }
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            //ATTEMPT
            BusinessEntityAddress? result = context.BusinessEntityAddress!.FirstOrDefault();

            //VERIFY
            Assert.NotNull(result);
        }


    }
}