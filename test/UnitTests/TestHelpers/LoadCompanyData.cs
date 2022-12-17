using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.UnitTests.Data;

namespace REA.Accounting.UnitTests.TestHelpers
{
    public static class LoadCompanyData
    {
        public static async Task SeedBusinessEntityAndCompany(this EfCoreContext context)
        {
            HashSet<BusinessEntity> businessEntities = await LoadTestData.LoadBusinessEntityDataAsync();
            await context.BusinessEntity!.AddRangeAsync(businessEntities);
            context.SaveChanges();
        }
    }
}