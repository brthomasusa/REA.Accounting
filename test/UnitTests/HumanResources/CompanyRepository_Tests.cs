#pragma warning disable CS8600, CS8625

using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.UnitTests.Data;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class CompanyRepository_Tests
    {
        private ICompanyRepository _repository;

        public CompanyRepository_Tests()
        {
            _repository = new CompanyRepository();
        }

        [Fact]
        public async Task GetCompanyByIdAsync_CompanyRepository_ShouldSucceed()
        {
            Company company = await _repository.GetCompanyByIdAsync(1);

            Assert.NotNull(company);
        }

        [Fact]
        public async Task GetEmployeesAsync_CompanyRepository_ShouldSucceed()
        {
            HashSet<Employee> employees = await _repository.GetEmployeesAsync();

            Assert.NotNull(employees);
            Assert.Equal(99, employees.Count);
        }

        [Fact]
        public async Task GetBusinessEntitiesAsync_CompanyRepository_ShouldSucceed()
        {
            HashSet<BusinessEntity> entities = await _repository.GetBusinessEntitiesAsync();

            Assert.NotNull(entities);
            Assert.Equal(33, entities.Count);
        }

        [Fact]
        public async Task GetAddressTypeAsync_CompanyRepository_ShouldSucceed()
        {
            HashSet<AddressType> entities = await _repository.GetAddressTypeAsync();

            Assert.NotNull(entities);
            Assert.Equal(6, entities.Count);
        }









        [Fact]
        public async Task LoadTestData_LoadBusinessEntityData_ShouldSucceed()
        {
            HashSet<BusinessEntity> businessEntities = await LoadTestData.LoadBusinessEntityDataAsync();

            Assert.NotNull(businessEntities);
        }

        [Fact]
        public async Task LoadTestData_LoadEmployeeData_ShouldSucceed()
        {
            HashSet<Employee> employees = await LoadTestData.LoadEmployeeDataAsync();

            Assert.NotNull(employees);
        }

        // [Fact]
        // public async Task LoadTestData_LoadDepartmentData_ShouldSucceed()
        // {
        //     List<Department> departments = await LoadTestData.LoadDepartmentDataAsync();

        //     Assert.NotNull(departments);
        // }

        // [Fact]
        // public async Task LoadTestData_LoadShiftData_ShouldSucceed()
        // {
        //     List<Shift> shifts = await LoadTestData.LoadShiftDataAsync();

        //     Assert.NotNull(shifts);
        // }

        // [Fact]
        // public async Task LoadTestData_LoadAddressData_ShouldSucceed()
        // {
        //     List<Address> addresses = await LoadTestData.LoadAddressData();

        //     Assert.NotNull(addresses);
        // }

        // [Fact]
        // public async Task LoadTestData_LoadEmailAddressData_ShouldSucceed()
        // {
        //     List<EmailAddress> emails = await LoadTestData.LoadEmailAddressData();

        //     Assert.NotNull(emails);
        // }

        // [Fact]
        // public async Task LoadTestData_LoadDepartmentHistoryData_ShouldSucceed()
        // {
        //     List<EmployeeDepartmentHistory> histories = await LoadTestData.LoadDepartmentHistoryData();

        //     Assert.NotNull(histories);
        // }

        // [Fact]
        // public async Task LoadTestData_LoadPayHistoryData_ShouldSucceed()
        // {
        //     List<EmployeePayHistory> histories = await LoadTestData.LoadPayHistoryData();

        //     Assert.NotNull(histories);
        // }

        // [Fact]
        // public async Task LoadTestData_LoadPersonData_ShouldSucceed()
        // {
        //     List<Person> people = await LoadTestData.LoadPersonData();

        //     Assert.NotNull(people);
        // }

        // [Fact]
        // public async Task LoadTestData_LoadTelephone_ShouldSucceed()
        // {
        //     List<PersonPhone> phones = await LoadTestData.LoadTelephoneDataAsync();

        //     Assert.NotNull(phones);
        // }

        // [Fact]
        // public void LoadTestData_LoadAll_ShouldSucceed()
        // {
        //     var exception = Record.ExceptionAsync(() => LoadTestData.LoadAllData());

        //     Assert.NotNull(exception);
        // }


    }
}