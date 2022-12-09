#pragma warning disable CS8600, CS8625

using Xunit;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.Core.Organization;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.UnitTests.Data;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class CompanyAggregate_Tests
    {
        private ICompanyRepository _repository;

        public CompanyAggregate_Tests()
        {
            _repository = new CompanyRepository();
        }

        [Fact]
        public async Task LoadTestData_LoadEmployeeData_ShouldSucceed()
        {
            List<Employee> employees = await LoadTestData.LoadEmployeeData();

            Assert.NotNull(employees);
        }

        [Fact]
        public async Task LoadTestData_LoadDepartmentData_ShouldSucceed()
        {
            List<Department> departments = await LoadTestData.LoadDepartmentData();

            Assert.NotNull(departments);
        }

        [Fact]
        public async Task GetCompanyByIdAsync_CompanyRepository_ShouldSucceed()
        {
            Company company = await _repository.GetCompanyByIdAsync(1);

            Assert.NotNull(company);
        }

        [Fact]
        public void Create_Employee_ShouldSucceed()
        {
            var exception = Record.Exception(() =>
                Employee.Create
                (
                    1,
                    "EM",
                    Core.Shared.NameStyleEnum.Western,
                    "Mr",
                    "John",
                    "Doe",
                    "D",
                    "Senior",
                    "358987145",
                    "adventure-works\\john10",
                    "/1/1/2",
                    "Tool Designer",
                    new DateOnly(1990, 2, 21),
                    "M",
                    "M",
                    new DateOnly(2011, 5, 30),
                    true,
                    80,
                    10,
                    true
                )
            );

            Assert.Null(exception);
        }

        [Fact]
        public void Create_Employee_Invalid_PersonType_ShouldFail()
        {
            var exception = Record.Exception(() =>
                Employee.Create
                (
                    1,
                    "IN",
                    Core.Shared.NameStyleEnum.Western,
                    "Mr",
                    "John",
                    "Doe",
                    "D",
                    "Senior",
                    "358987145",
                    "adventure-works\\john10",
                    "/1/1/2",
                    "Tool Designer",
                    new DateOnly(1990, 2, 21),
                    "M",
                    "M",
                    new DateOnly(2011, 5, 30),
                    true,
                    80,
                    10,
                    true
                )
            );

            Assert.NotNull(exception);
        }


        [Fact]
        public void Create_Employee_Invalid_BirthDate_ShouldFail()
        {
            var exception = Record.Exception(() =>
                Employee.Create
                (
                    1,
                    "EM",
                    Core.Shared.NameStyleEnum.Western,
                    "Mr",
                    "John",
                    "Doe",
                    "D",
                    "Senior",
                    "358987145",
                    "adventure-works\\john10",
                    "/1/1/2",
                    "Tool Designer",
                    new DateOnly(2005, 2, 21),
                    "M",
                    "M",
                    new DateOnly(2011, 5, 30),
                    true,
                    80,
                    10,
                    true
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Update_Employee_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();

            var exception = Record.Exception(() => employee.UpdateFirstName("Jonny"));

            Assert.Null(exception);
        }

        [Fact]
        public void Update_Employee_Invalid_PersonType_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();

            var exception = Record.Exception(() => employee.UpdatePersonType("IN"));

            Assert.NotNull(exception);
        }

        private Employee GetEmployeeForEditing()
            => Employee.Create
                (
                    1,
                    "EM",
                    Core.Shared.NameStyleEnum.Western,
                    "Mr",
                    "John",
                    "Doe",
                    "D",
                    "Senior",
                    "358987145",
                    "adventure-works\\john10",
                    "/1/1/2",
                    "Tool Designer",
                    new DateOnly(1990, 2, 21),
                    "M",
                    "M",
                    new DateOnly(2011, 5, 30),
                    true,
                    80,
                    10,
                    true
                );
    }
}

/* 
    int employeeID,
    string personType,
    NameStyleEnum nameStyle,
    string title,
    string firstName,
    string lastName,
    string middleName,
    string suffix,
    string nationalID,
    string login,
    string orgNode,
    string jobTitle,
    DateOnly birthDate,
    string maritalStatus,
    string gender,
    DateOnly hireDate,
    bool salaried,
    int vacation,
    int sickLeave,
    bool active
*/



