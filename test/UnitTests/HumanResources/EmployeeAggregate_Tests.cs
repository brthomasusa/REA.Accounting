#pragma warning disable CS8600, CS8625

using Xunit;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.Core.Shared;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class EmployeeAggregate_Tests
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
        public void Create_PayHistory_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<PayHistory> result = employee.AddPayHistory(1, new DateTime(2023, 1, 7), 15.00M, PayFrequencyEnum.BiWeekly);

            Assert.True(result.Success);
        }

        [Fact]
        public void Create_PayHistory_Default_RateChangeDate_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<PayHistory> result = employee.AddPayHistory(1, new DateTime(), 15.00M, PayFrequencyEnum.BiWeekly);

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_PayHistory_Invalid_Rate_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<PayHistory> result = employee.AddPayHistory(1, new DateTime(2023, 1, 7), 6.49M, PayFrequencyEnum.BiWeekly);

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_PayHistory_Invalid_PayFrequency_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<PayHistory> result = employee.AddPayHistory(1, new DateTime(2023, 1, 7), 15.00M, 0);

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_DepartmentHistory_ValidEndDate_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<DepartmentHistory> result = employee.AddDepartmentHistory(1, 1, new DateOnly(2023, 1, 7), new DateTime(2023, 1, 8));

            Assert.True(result.Success);
        }

        [Fact]
        public void Create_DepartmentHistory_DefaultEndDate_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<DepartmentHistory> result = employee.AddDepartmentHistory(1, 1, new DateOnly(2023, 1, 7), new DateTime());

            Assert.True(result.Success);
        }

        [Fact]
        public void Create_DepartmentHistory_NullEndDate_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<DepartmentHistory> result = employee.AddDepartmentHistory(1, 1, new DateOnly(2023, 1, 7), null);

            Assert.True(result.Success);
        }

        [Fact]
        public void Create_DepartmentHistory_StartDateAfterEndDate_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<DepartmentHistory> result = employee.AddDepartmentHistory(1, 1, new DateOnly(2023, 1, 7), new DateTime(2023, 1, 1));

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_DepartmentHistory_DefaultStartDate_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();
            OperationResult<DepartmentHistory> result = employee.AddDepartmentHistory(1, 1, new DateOnly(), new DateTime());

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_Address_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.Address> result =
                employee.AddAddress(
                    1, 1, AddressTypeEnum.Home, "123 Main", null, "Somewhere", 1, "12345"
                );

            Assert.True(result.Success);
        }

        [Fact]
        public void Create_Address_NullLine1ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.Address> result =
                employee.AddAddress(
                    1, 1, AddressTypeEnum.Home, null, null, "Somewhere", 1, "12345"
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_Address_NullCity_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.Address> result =
                employee.AddAddress(
                    1, 1, AddressTypeEnum.Home, "123 Main", null, null, 1, "12345"
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_Address_InvalidAddressType_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.Address> result =
                employee.AddAddress(
                    1, 1, 0, "123 Main", null, "Somewhere", 1, "12345"
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_Address_InvalidStateCode_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.Address> result =
                employee.AddAddress(
                    1, 1, AddressTypeEnum.Home, "123 Main", null, "Somewhere", 0, "12345"
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_EmailAddress_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.PersonEmailAddress> result =
                employee.AddEmailAddress(
                    1, 1, "joe4@adventureworks.com"
                );

            Assert.True(result.Success);
        }

        [Fact]
        public void Create_EmailAddress_NullEmail_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.PersonEmailAddress> result =
                employee.AddEmailAddress(
                    1, 1, null
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_EmailAddress_InvalidEmail_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.PersonEmailAddress> result =
                employee.AddEmailAddress(
                    1, 1, "joesemail"
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_PersonPhone_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.PersonPhone> result =
                employee.AddPhoneNumbers(
                    1, PhoneNumberTypeEnum.Home, "555-555-5555"
                );

            Assert.True(result.Success);
        }

        [Fact]
        public void Create_PersonPhone_InvalidPhoneType_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.PersonPhone> result =
                employee.AddPhoneNumbers(
                    1, 0, "555-555-5555"
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_PersonPhone_NullPhoneNumber_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.PersonPhone> result =
                employee.AddPhoneNumbers(
                    1, PhoneNumberTypeEnum.Home, null
                );

            Assert.False(result.Success);
        }

        [Fact]
        public void Create_PersonPhone_InvalidPhoneNumber_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();

            OperationResult<REA.Accounting.Core.Shared.PersonPhone> result =
                employee.AddPhoneNumbers(
                    1, PhoneNumberTypeEnum.Home, "(789)-555-555-5555"
                );

            Assert.False(result.Success);
        }

        private Employee GetEmployeeForCreate()
            => Employee.Create
                (
                    0,
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
                    new DateOnly(2023, 1, 5),
                    true,
                    0,
                    0,
                    true
                );


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



