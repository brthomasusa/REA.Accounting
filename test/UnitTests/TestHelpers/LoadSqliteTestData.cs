using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.Sales;

namespace REA.Accounting.UnitTests.TestHelpers
{
    public static class LoadSqliteTestData
    {
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

        public static async Task SeedPersonAndHrData(this EfCoreContext context)
        {
            HashSet<BusinessEntity> businessEntities = await Data.LoadTestData.LoadBusinessEntityDataAsync();
            HashSet<PersonDataModel> people = await Data.LoadTestData.LoadPersonDataAsync();
            HashSet<Address> addresses = await Data.LoadTestData.LoadAddressDataAsync();
            HashSet<BusinessEntityAddress> businessEntityAddresses = await Data.LoadTestData.LoadBusinessEntityAddressDataAsync();
            HashSet<EmailAddress> emailAddresses = await Data.LoadTestData.LoadEmailAddressDataAsync();
            HashSet<PersonPhone> telephones = await Data.LoadTestData.LoadPhoneDataAsync();
            HashSet<Employee> employees = await Data.LoadTestData.LoadEmployeeDataAsync();
            HashSet<EmployeeDepartmentHistory> departmentHistories = await Data.LoadTestData.LoadEmployeeDepartmentHistoryDataAsync();
            HashSet<EmployeePayHistory> payHistories = await Data.LoadTestData.LoadEmployeePayHistoryDataAsync();

            await context.BusinessEntity!.AddRangeAsync(businessEntities);
            await context.Person!.AddRangeAsync(people);
            await context.Address!.AddRangeAsync(addresses);
            await context.BusinessEntityAddress!.AddRangeAsync(businessEntityAddresses);
            await context.EmailAddress!.AddRangeAsync(emailAddresses);
            await context.PersonPhone!.AddRangeAsync(telephones);
            await context.Employee!.AddRangeAsync(employees);
            await context.EmployeeDepartmentHistory!.AddRangeAsync(departmentHistories);
            await context.EmployeePayHistory!.AddRangeAsync(payHistories);

            await context.SaveChangesAsync();
        }
    }
}