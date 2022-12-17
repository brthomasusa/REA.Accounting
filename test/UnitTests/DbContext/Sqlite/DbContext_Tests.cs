using REA.Accounting.UnitTests.TestHelpers;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.UnitTests.DbContext.Sqlite
{
    public class DbContext_Tests
    {
        [Fact]
        public async Task TestSQLite_Setup_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using (var context = new EfCoreContext(options))
            {
                context.Database.EnsureCreated();
                await context.SeedBusinessEntityAndCompany();

                //ATTEMPT
                BusinessEntity? businessEntity = context.BusinessEntity!.Find(290);

                //VERIFY
                Assert.Equal(new Guid("de909360-485d-4fc5-9935-bef2074e7493"), businessEntity!.RowGuid);
            }
        }

        [Fact]
        public async Task Create_BusinessEntity_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using (var context = new EfCoreContext(options))
            {
                context.Database.EnsureCreated();

                //ATTEMPT
                DateTime modified = new DateTime(2022, 12, 16);
                BusinessEntity? businessEntity = new() { BusinessEntityID = 291, RowGuid = Guid.NewGuid(), ModifiedDate = modified };
                context.BusinessEntity!.Add(businessEntity);
                await context.SaveChangesAsync();

                BusinessEntity? result = context.BusinessEntity!.FirstOrDefault();

                //VERIFY
                Assert.NotNull(result);
                Console.WriteLine($"ID: {result!.BusinessEntityID}");
            }
        }

        // [Fact]
        // public async Task Create_Company_ShouldSucceed()
        // {
        //     //SETUP
        //     var options = SqliteInMemory.CreateOptions<EfCoreContext>();

        //     using (var context = new EfCoreContext(options))
        //     {
        //         context.Database.EnsureCreated();

        //         //ATTEMPT
        //         DateTime modified = new DateTime(2022, 12, 16);
        //         BusinessEntity? businessEntity = new() { BusinessEntityID = 0, RowGuid = Guid.NewGuid(), ModifiedDate = modified };
        //         context.BusinessEntity!.Add(businessEntity);
        //         await context.SaveChangesAsync();
        //         // Company company = new()
        //         // {

        //         // };

        //         //VERIFY
        //         Assert.Equal(new Guid("de909360-485d-4fc5-9935-bef2074e7493"), businessEntity!.RowGuid);
        //     }
        // }
    }
}

/*
public int BusinessEntityID { get; set; }
public string? CompanyName { get; set; }
public string? LegalName { get; set; }
public string? EIN { get; set; }
public string? WebsiteUrl { get; set; }
public Guid RowGuid { get; set; }
public DateTime ModifiedDate { get; set; }
*/