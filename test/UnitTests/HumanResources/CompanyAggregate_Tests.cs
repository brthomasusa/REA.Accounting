#pragma warning disable CS8600, CS8625

using Xunit;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class CompanyAggregate_Tests
    {
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



