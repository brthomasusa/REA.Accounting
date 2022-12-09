using REA.Accounting.Core.Organization;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.UnitTests.Data;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class CompanyRepository : ICompanyRepository
    {
        private List<Employee>? _employees;
        private List<Department>? _departments;
        private List<Shift>? _shifts;

        public CompanyRepository()
        {
            EmployeesAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            Company company = Company.Create
            (
                1,
                OrganizationName.Create("AdventureWorks Cycles"),
                OrganizationName.Create("AdventureWorks Cycles, Inc"),
                EmployerIdentificationNumber.Create("12-3456789"),
                WebsiteUrl.Create("https://www.AdventureWorksCycles.com"),
                _employees!
            );

            return await Task.FromResult(company);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
            => await Task.FromResult(_employees!);

        private async void EmployeesAsync()
            => _employees = await LoadTestData.LoadEmployeeData();
    }
}