using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.IntegrationTests.Base
{
    public static class EmployeeTestData
    {
        public static CreateEmployeeCommand GetValidCreateEmployeeCommand()
            => new(
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                EmailPromotion: 2,
                0,
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

        public static CreateEmployeeCommand GetInvalidCreateEmployeeCommand_Under18()
            => new(
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Mr.",
                FirstName: "Johnny",
                LastName: "Doe",
                MiddleName: "J",
                Suffix: null,
                EmailPromotion: 2,
                0,
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

        public static CreateEmployeeCommand GetInvalidCreateEmployeeCommand_DupeEmployeeName()
            => new(
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Ms  .",
                FirstName: "Laura",
                LastName: "Norman",
                MiddleName: "F",
                Suffix: null,
                EmailPromotion: 2,
                0,
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

        public static CreateEmployeeCommand GetInvalidCreateEmployeeCommand_DupeNationalID()
            => new(
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Ms  .",
                FirstName: "Laureen",
                LastName: "Jones",
                MiddleName: "F",
                Suffix: null,
                EmailPromotion: 2,
                0,
                NationalID: "295847284",
                Login: @"adventure-works\johnny0",
                JobTitle: "The Man",
                BirthDate: new DateTime(2000, 1, 31),
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

        public static CreateEmployeeCommand GetInvalidCreateEmployeeCommand_DupeEmail()
            => new(
                EmployeeID: 0,
                PersonType: "EM",
                NameStyle: false,
                Title: "Ms  .",
                FirstName: "Laureen",
                LastName: "Jones",
                MiddleName: "F",
                Suffix: null,
                EmailPromotion: 2,
                0,
                NationalID: "295847284",
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
                EmailAddress: "jean0@adventure-works.com",
                PhoneNumber: "555-555-5555",
                PhoneNumberType: 2
            );

        public static UpdateEmployeeCommand GetUpdateEmployeeCommand_ValidData()
            => new(
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

        public static BusinessEntity GetBusinessEntity()
            => new()
            {
                BusinessEntityID = 0,
                PersonModel = new()
                {
                    PersonType = "EM",
                    NameStyle = false,
                    Title = "Mr.",
                    FirstName = "Johnny",
                    MiddleName = "D",
                    LastName = "Doe",
                    Suffix = "Jr.",
                    EmailPromotion = 2,
                    Employee = new EmployeeDataModel()
                    {
                        NationalIDNumber = "123797967",
                        LoginID = @"adventure-works\johnny01",
                        JobTitle = "Vice President At Large",
                        BirthDate = new DateTime(1971, 8, 1),
                        MaritalStatus = "M",
                        Gender = "M",
                        HireDate = new DateTime(2008, 1, 31),
                        SalariedFlag = true,
                        VacationHours = 1,
                        SickLeaveHours = 20,
                        CurrentFlag = true
                    },
                    EmailAddresses = new List<EmailAddress>()
                    {
                        new EmailAddress
                        {
                            MailAddress = "johnnydoe@adventureworks.com"
                        }
                    },
                    Telephones = new List<PersonPhone>()
                    {
                        new PersonPhone { PhoneNumber = "214-555-4567", PhoneNumberTypeID = 1},
                        new PersonPhone { PhoneNumber = "972-555-1234", PhoneNumberTypeID = 2},
                        new PersonPhone { PhoneNumber = "469-555-4567", PhoneNumberTypeID = 3}
                    }
                }
            };
    }
}