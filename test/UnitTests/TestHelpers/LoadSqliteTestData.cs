using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.Sales;
using REA.Accounting.UnitTests.Data;

namespace REA.Accounting.UnitTests.TestHelpers
{
    public static class LoadSqliteTestData
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

        public static async Task SeedLookupData(this EfCoreContext context)
        {
            HashSet<CountryRegion> countryRegions = await Data.LoadTestData.LoadCountryRegionDataAsync();
            HashSet<SalesTerritory> salesTerritories = await Data.LoadTestData.LoadSalesTerritoryDataAsync();
            HashSet<StateProvince> stateProvinces = await Data.LoadTestData.LoadStateProvinceDataAsync();

            await context.CountryRegion!.AddRangeAsync(countryRegions);
            await context.SalesTerritory!.AddRangeAsync(salesTerritories);
            await context.StateProvince!.AddRangeAsync(stateProvinces);

            await context.SaveChangesAsync();
        }

        public static async Task SeedPersonData(this EfCoreContext context)
        {
            HashSet<BusinessEntity> businessEntities = await Data.LoadTestData.LoadBusinessEntityDataAsync();
            HashSet<PersonDataModel> people = await Data.LoadTestData.LoadPersonDataAsync();
            HashSet<Address> addresses = await Data.LoadTestData.LoadAddressDataAsync();
            HashSet<BusinessEntityAddress> businessEntityAddresses = await Data.LoadTestData.LoadBusinessEntityAddressDataAsync();
            HashSet<EmailAddress> emailAddresses = await Data.LoadTestData.LoadEmailAddressDataAsync();

            await context.BusinessEntity!.AddRangeAsync(businessEntities);
            await context.Person!.AddRangeAsync(people);
            await context.Address!.AddRangeAsync(addresses);
            await context.BusinessEntityAddress!.AddRangeAsync(businessEntityAddresses);
            await context.EmailAddress!.AddRangeAsync(emailAddresses);

            await context.SaveChangesAsync();
        }

        public static async Task SeedEmailAddressData(this EfCoreContext context)
        {
            HashSet<BusinessEntity> businessEntities = await Data.LoadTestData.LoadBusinessEntityDataAsync();
            HashSet<EmailAddress> emailAddresses = await Data.LoadTestData.LoadEmailAddressDataAsync();

            await context.BusinessEntity!.AddRangeAsync(businessEntities);
            await context.EmailAddress!.AddRangeAsync(emailAddresses);

            await context.SaveChangesAsync();
        }
    }
}