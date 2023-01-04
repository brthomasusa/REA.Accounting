#pragma warning disable CS8625

using REA.Accounting.Core.Shared;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.UnitTests.Shared
{
    public class SharedEntityObject_Tests
    {
        [Fact]
        public void Person_AddAddress_ShouldSucceed()
        {
            Person person = GetPersonForEditing();
            OperationResult<Address> result = person.AddAddress
            (
                0,
                1,
                AddressTypeEnum.Home,
                "123 Main Street",
                "Ste 1",
                "Somewhereville",
                1,
                "12345"
            );

            Assert.True(result.Success);
        }

        [Fact]
        public void Address_Create_Invalid_NullLine1_ShouldFail()
        {
            Person person = GetPersonForEditing();
            OperationResult<Address> result = person.AddAddress
            (
                0,
                1,
                AddressTypeEnum.Home,
                null,
                "Ste 1",
                "Somewhereville",
                1,
                "12345"
            );

            Assert.False(result.Success);
        }

        [Fact]
        public void Address_Create_Invalid_BadStateProvinceID_ShouldFail()
        {
            Person person = GetPersonForEditing();
            OperationResult<Address> result = person.AddAddress
            (
                0,
                1,
                AddressTypeEnum.Home,
                "123 Main Street",
                "Ste 1",
                "Somewhereville",
                0,
                "12345"
            );

            Assert.False(result.Success);
        }

        [Fact]
        public void Address_Create_Invalid_NullPostalCode_ShouldFail()
        {
            Person person = GetPersonForEditing();
            OperationResult<Address> result = person.AddAddress
            (
                0,
                1,
                AddressTypeEnum.Home,
                "123 Main Street",
                "Ste 1",
                "Somewhereville",
                1,
                null
            );

            Assert.False(result.Success);
        }

        [Fact]
        public void Address_Update_ShouldSucceed()
        {
            Person person = GetPersonForEditing();
            OperationResult<Address> result = person.UpdateAddress
            (
                1,
                2,
                AddressTypeEnum.Home,
                "123 Main Street",
                null,
                "Somewhereville",
                1,
                "12345"
            );

            Assert.False(result.Success);
        }

        [Fact]
        public void Person_Create_Valid_ShouldSucceed()
        {
            Person person = Contact.Create
            (
                1,
                ContactTypeEnum.AccountingManager,
                0,
                "SC",
                NameStyleEnum.Western,
                "Mr.",
                "Sandy",
                "S",
                "Jones",
                "III",
                EmailPromotionEnum.None
            );

            Assert.IsType<Contact>(person);
        }

        [Fact]
        public void Person_Create_NullTitle_ShouldSucceed()
        {
            Person person = Contact.Create
            (
                1,
                ContactTypeEnum.AccountingManager,
                0,
                "SC",
                NameStyleEnum.Western,
                null,
                "Sandy",
                "S",
                "Jones",
                "III",
                EmailPromotionEnum.None
            );

            Assert.IsType<Contact>(person);
        }

        [Fact]
        public void Person_Create_NullSuffix_ShouldSucceed()
        {
            Person person = Contact.Create
            (
                1,
                ContactTypeEnum.AccountingManager,
                0,
                "SC",
                NameStyleEnum.Western,
                "Mr",
                "Sandy",
                "S",
                "Jones",
                null,
                EmailPromotionEnum.None
            );

            Assert.IsType<Contact>(person);
        }

        [Fact]
        public void Person_Create_NullMiddleName_ShouldSucceed()
        {
            Person person = Contact.Create
            (
                1,
                ContactTypeEnum.AccountingManager,
                0,
                "SC",
                NameStyleEnum.Western,
                "Mr",
                "Sandy",
                null,
                "Jones",
                "III",
                EmailPromotionEnum.None
            );

            Assert.IsType<Contact>(person);
        }

        [Fact]
        public void Person_Create_NullContactType_ShouldFail()
        {
            Action action = () => Contact.Create
            (
                1,
                ContactTypeEnum.AccountingManager,
                0,
                null,
                NameStyleEnum.Western,
                "Mr",
                "Sandy",
                "H",
                "Jones",
                "III",
                EmailPromotionEnum.None
            );

            var caughtException = Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Person_Create_InvalidContactType_ShouldFail()
        {
            Action action = () => Contact.Create
            (
                1,
                ContactTypeEnum.AccountingManager,
                0,
                "XX",
                NameStyleEnum.Western,
                "Mr",
                "Sandy",
                "H",
                "Jones",
                "III",
                EmailPromotionEnum.None
            );

            var caughtException = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void PersonPhone_Create_ValidData_ShouldSucceed()
        {
            Employee employee = GetEmployeeForEditing();
            string phoneNumber = "214-555-5555";

            OperationResult<PersonPhone> result = employee.AddPhoneNumbers(1, PhoneNumberTypeEnum.Cell, phoneNumber);

            Assert.True(result.Success);
        }

        [Fact]
        public void PersonPhone_Create_Invalid_TooLong_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();
            string phoneNumber = "214-555-5555123145214514787771234785477777";

            OperationResult<PersonPhone> result = employee.AddPhoneNumbers(1, PhoneNumberTypeEnum.Cell, phoneNumber);

            Assert.False(result.Success);
        }

        [Fact]
        public void PersonPhone_Create_Invalid_PhoneType_ShouldFail()
        {
            Employee employee = GetEmployeeForEditing();
            string phoneNumber = "214-555-5555";

            OperationResult<PersonPhone> result = employee.AddPhoneNumbers(1, 0, phoneNumber);

            Assert.False(result.Success);
        }

        private Person GetPersonForEditing()
            => Contact.Create
            (
                1,
                ContactTypeEnum.AccountingManager,
                0,
                "SC",
                NameStyleEnum.Western,
                "Mr.",
                "Sandy",
                "S",
                "Jones",
                "III",
                EmailPromotionEnum.None
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