using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.UnitTests.Data;

namespace REA.Accounting.UnitTests.TestHelpers
{
    public static class LoadTestData
    {
        public static async Task SeedBusinessEntities(this EfCoreContext context)
        {
            HashSet<BusinessEntity> businessEntities = await Data.LoadTestData.LoadBusinessEntityDataAsync();
            await context.BusinessEntity!.AddRangeAsync(businessEntities);
            await context.SaveChangesAsync();
        }

        public static async Task SeedBusinessEntityAndCompany(this EfCoreContext context)
        {
            BusinessEntity entity = new()
            {
                BusinessEntityID = 1,
                Company = new()
                {
                    CompanyName = "AdventureWorks Cycles",
                    LegalName = "AdventureWorks Cycles LLC",
                    EIN = "12-3456789",
                    WebsiteUrl = "https:\\www.Adventureworkscycles.com",
                    RowGuid = new Guid("734a8aa4-0686-429c-8192-8bbd214132b7"),
                    ModifiedDate = DateTime.Now
                },
                RowGuid = new Guid("e57b0258-87b9-4daf-8e03-ad070178140a"),
                ModifiedDate = DateTime.Now
            };

            await context.BusinessEntity!.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public static async Task SeedPersonLookupData(this EfCoreContext context)
        {
            HashSet<AddressType> addressTypes = await Data.LoadTestData.LoadAddressTypeDataAsync();
            await context.AddressType!.AddRangeAsync(addressTypes);
            await context.SaveChangesAsync();
        }
    }
}