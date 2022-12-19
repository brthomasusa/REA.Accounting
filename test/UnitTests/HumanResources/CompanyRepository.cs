using REA.Accounting.UnitTests.Data;
using REA.Accounting.Infrastructure.Persistence.DataModels.Sales;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class CompanyRepository : ICompanyRepository
    {
        private HashSet<BusinessEntity>? _businessEntities;
        private HashSet<AddressType>? _addressTypes;
        private HashSet<Employee>? _employees;


        // private List<Department>? _departments;
        // private List<Shift>? _shifts;

        public CompanyRepository()
        {
            BusinessEntitiesAsync();
            EmployeesAsync();
            AddressTypesAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            Company company = new Company
            {
                BusinessEntityID = 1,
                CompanyName = "AdventureWorks Cycles",
                LegalName = "AdventureWorks Cycles, Inc",
                EIN = "12-3456789",
                WebsiteUrl = "https://www.AdventureWorksCycles.com",
                RowGuid = Guid.NewGuid(),
                ModifiedDate = new DateTime(2022, 1, 1)
            };

            return await Task.FromResult(company);
        }

        public async Task<HashSet<Employee>> GetEmployeesAsync()
            => await Task.FromResult(_employees!);

        public async Task<HashSet<BusinessEntity>> GetBusinessEntitiesAsync()
            => await Task.FromResult(_businessEntities!);

        public async Task<HashSet<AddressType>> GetAddressTypeAsync()
            => await Task.FromResult(_addressTypes!);

        private async void EmployeesAsync()
            => _employees = await LoadTestData.LoadEmployeeDataAsync();

        private async void BusinessEntitiesAsync()
            => _businessEntities = await LoadTestData.LoadBusinessEntityDataAsync();

        private async void AddressTypesAsync()
            => _addressTypes = await LoadTestData.LoadAddressTypeDataAsync();
    }
}