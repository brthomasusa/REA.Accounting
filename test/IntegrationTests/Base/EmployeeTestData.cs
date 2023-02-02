using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.UpdateEmployee;

namespace REA.Accounting.IntegrationTests.Base
{
    public static class EmployeeTestData
    {
        public static CreateEmployeeCommand GetValidCreateEmployeeCommand()
            => new CreateEmployeeCommand
            (
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                EmailPromotion: 2,
                NationalID: "13232145",
                Login: @"adventure-works\johnny0",
                JobTitle: "The Man",
                BirthDate: new DateTime(2003, 1, 17),
                MaritalStatus: "M",
                Gender: "M",
                HireDate: new DateTime(2020, 1, 28),
                Salaried: true,
                Vacation: 5,
                SickLeave: 1,
                Active: true,
                PayRate: 20.00M,
                PayFrequency: 1,
                DepartmentID: 1,
                ShiftID: 1,
                AddressType: 2,
                AddressLine1: "123 street",
                AddressLine2: "Apt 123",
                City: "Somewhere",
                StateCode: 73,
                PostalCode: "12345",
                EmailAddress: "johnny@adventure-works.com",
                PhoneNumber: "555-555-5555",
                PhoneNumberType: 2
            );

        public static CreateEmployeeCommand GetInvalidCreateEmployeeCommand()
            => new CreateEmployeeCommand
            (
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                EmailPromotion: 2,
                NationalID: "13232145",
                Login: @"adventure-works\johnny0",
                JobTitle: "The Man",
                BirthDate: new DateTime(2006, 1, 31),
                MaritalStatus: "M",
                Gender: "M",
                HireDate: new DateTime(2020, 1, 28),
                Salaried: true,
                Vacation: 5,
                SickLeave: 1,
                Active: true,
                PayRate: 20.00M,
                PayFrequency: 1,
                DepartmentID: 1,
                ShiftID: 1,
                AddressType: 2,
                AddressLine1: "123 street",
                AddressLine2: "Apt 123",
                City: "Somewhere",
                StateCode: 73,
                PostalCode: "12345",
                EmailAddress: "johnny@adventure-works.com",
                PhoneNumber: "555-555-5555",
                PhoneNumberType: 2
            );

        public static UpdateEmployeeCommand GetUpdateEmployeeCommand()
            => new UpdateEmployeeCommand
            (
                EmployeeID: 273,
                PersonType: "EM",
                NameStyle: false,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                EmailPromotion: 2,
                NationalID: "112432117",
                Login: @"adventure-works\johnny0",
                JobTitle: "The Man",
                BirthDate: new DateTime(2000, 1, 28),
                MaritalStatus: "M",
                Gender: "M",
                HireDate: new DateTime(2020, 1, 28),
                Salaried: true,
                Vacation: 5,
                SickLeave: 1,
                Active: true
            );
    }
}