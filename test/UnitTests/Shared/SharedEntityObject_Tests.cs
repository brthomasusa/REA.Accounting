#pragma warning disable CS8625

using Xunit;
using REA.Accounting.Core.Shared;
using REA.Accounting.Core.Shared.ValueObjects;

namespace REA.Accounting.UnitTests.Shared
{
    public class SharedEntityObject_Tests
    {
        [Fact]
        public void Address_Create_Valid_ShouldSucceed()
        {
            Address address = Address.Create
            (
                0,
                "123 Main Street",
                "Ste 1",
                "Somewhereville",
                1,
                "12345"
            );

            Assert.IsType<Address>(address);
        }

        [Fact]
        public void Address_Create_Valid_NullLine2_ShouldSucceed()
        {
            Address address = Address.Create
            (
                0,
                "123 Main Street",
                null,
                "Somewhereville",
                1,
                "12345"
            );

            Assert.IsType<Address>(address);
        }

        [Fact]
        public void Address_Create_Invalid_NullLine1_ShouldFail()
        {
            Action action = () => Address.Create
            (
                0,
                null,
                "Ste 1",
                "Somewhereville",
                1,
                "12345"
            );

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Address line 1 can not be null or greater than 60 characters.", caughtException.Message);
        }

        [Fact]
        public void Address_Create_Invalid_BadStateProvinceID_ShouldFail()
        {
            Action action = () => Address.Create
            (
                0,
                "123 Main Street",
                "Ste 1",
                "Somewhereville",
                0,
                "12345"
            );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Equal("A state/province id is required.", caughtException.ParamName);
        }

        [Fact]
        public void Address_Create_Invalid_NullPostalCode_ShouldFail()
        {
            Action action = () => Address.Create
            (
                0,
                "123 Main Street",
                "Ste 1",
                "Somewhereville",
                1,
                null
            );

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Postal code can not be null or greater than 15 characters.", caughtException.Message);
        }

        [Fact]
        public void Address_Update_Valid_ShouldSucceed()
        {
            Address address = GetAddressForEditing();

            var exception = Record.Exception(() => address.UpdateAddressLine1("321 3rd Ave"));
            Assert.Null(exception);

            exception = Record.Exception(() => address.UpdateAddressLine2("3rd Floor"));
            Assert.Null(exception);

            exception = Record.Exception(() => address.UpdateCity("San Angelo"));
            Assert.Null(exception);

            exception = Record.Exception(() => address.UpdateStateProvinceId(2));
            Assert.Null(exception);

            exception = Record.Exception(() => address.UpdatePostalCode("9874561"));
            Assert.Null(exception);
        }

        [Fact]
        public void Address_Update_Invalid_Line1_ShouldFail()
        {
            Address address = GetAddressForEditing();

            Action action = () => address.UpdateAddressLine1(null);
            var caughtException = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void Address_Update_Invalid_Line2_ShouldFail()
        {
            Address address = GetAddressForEditing();

            Action action = () => address.UpdateAddressLine2("12345678901234567890123456789012345678901234567890123456789012");
            var caughtException = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void Address_Update_Invalid_City_ShouldFail()
        {
            Address address = GetAddressForEditing();

            Action action = () => address.UpdateCity(null);
            var caughtException = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void Address_Update_Invalid_StateProvinceId_ShouldFail()
        {
            Address address = GetAddressForEditing();

            Action action = () => address.UpdateStateProvinceId(0);
            var caughtException = Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Address_Update_Invalid_PostalCode_ShouldFail()
        {
            Address address = GetAddressForEditing();

            Action action = () => address.UpdatePostalCode(null);
            var caughtException = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void Person_Create_Valid_ShouldSucceed()
        {
            Person person = Person.Create
            (
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

            Assert.IsType<Person>(person);
        }

        [Fact]
        public void Person_Create_NullTitle_ShouldSucceed()
        {
            Person person = Person.Create
            (
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

            Assert.IsType<Person>(person);
        }

        [Fact]
        public void Person_Create_NullSuffix_ShouldSucceed()
        {
            Person person = Person.Create
            (
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

            Assert.IsType<Person>(person);
        }

        [Fact]
        public void Person_Create_NullMiddleName_ShouldSucceed()
        {
            Person person = Person.Create
            (
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

            Assert.IsType<Person>(person);
        }

        [Fact]
        public void Person_Create_NullContactType_ShouldFail()
        {
            Action action = () => Person.Create
            (
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
            Action action = () => Person.Create
            (
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
        public void Person_Update_ContactType_ShouldSucceed()
        {
            Person person = GetPersonForEditing();

            var exception = Record.Exception(() => person.UpdatePersonType("IN"));
            Assert.Null(exception);
        }

        [Fact]
        public void Person_Update_ContactType_InvalidContactType_ShouldFail()
        {
            Person person = GetPersonForEditing();

            var exception = Record.Exception(() => person.UpdatePersonType("1N"));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Person_Update_ContactType_Null_ContactType_ShouldFail()
        {
            Person person = GetPersonForEditing();

            var exception = Record.Exception(() => person.UpdatePersonType(null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Person_Update_LastName_ShouldSucceed()
        {
            Person person = GetPersonForEditing();

            var exception = Record.Exception(() => person.UpdateLastName("Jonas"));
            Assert.Null(exception);
        }

        [Fact]
        public void Person_Update_FirstName_ShouldSucceed()
        {
            Person person = GetPersonForEditing();

            var exception = Record.Exception(() => person.UpdateFirstName("Jonanne"));
            Assert.Null(exception);
        }

        [Fact]
        public void Person_Update_EmptyStr_LastName_ShouldFail()
        {
            Person person = GetPersonForEditing();

            Action action = () => person.UpdateLastName("");
            var caughtException = Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Person_Update_Null_LastName_ShouldFail()
        {
            Person person = GetPersonForEditing();

            Action action = () => person.UpdateLastName(null);
            var caughtException = Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Person_Update_NameStyle_WithZero_ShouldSucceed()
        {
            Person person = GetPersonForEditing();

            var exception = Record.Exception(() => person.UpdateNameStyle(0));
            Assert.Null(exception);
        }

        [Fact]
        public void PersonPhone_Create_ValidData_ShouldSucceed()
        {
            string phoneNumber = "214-555-5555";

            PersonPhone phone = PersonPhone.Create
            (
                1,
                PhoneNumberTypeEnum.Cell,
                phoneNumber
            );

            Assert.IsType<PersonPhone>(phone);
        }

        [Fact]
        public void PersonPhone_Create_Invalid_TooLong_ShouldFail()
        {
            string phoneNumber = "214-555-5555123145214514787771234785477777";

            var exception = Record.Exception(() => PersonPhone.Create
            (
                1,
                0,
                phoneNumber
            ));

            Assert.NotNull(exception);
        }

        [Fact]
        public void PersonPhone_Create_Invalid_PhoneType_ShouldFail()
        {
            string phoneNumber = "214-555-5555";

            var exception = Record.Exception(() => PersonPhone.Create
            (
                1,
                0,
                phoneNumber
            ));

            Assert.NotNull(exception);
        }

        [Fact]
        public void PersonPhone_Update_PhoneType_ShouldSucceed()
        {
            PersonPhone phone = GetPersonPhoneForEditing();

            var exception = Record.Exception(() => phone.UpdatePhoneNumberType(PhoneNumberTypeEnum.Home));
            Assert.Null(exception);
        }

        [Fact]
        public void PersonPhone_Update_PhoneType_ShouldFail()
        {
            PersonPhone phone = GetPersonPhoneForEditing();

            var exception = Record.Exception(() => phone.UpdatePhoneNumberType(0));
            Assert.NotNull(exception);
        }

        [Fact]
        public void PersonPhone_Update_TelephoneNumber_ShouldSucceed()
        {
            PersonPhone phone = GetPersonPhoneForEditing();

            var exception = Record.Exception(() => phone.UpdateTelephone("817-932-6541"));
            Assert.Null(exception);
        }

        [Fact]
        public void PersonPhone_Update_TelephoneNumber_Null_ShouldFail()
        {
            PersonPhone phone = GetPersonPhoneForEditing();

            var exception = Record.Exception(() => phone.UpdateTelephone(null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void PersonPhone_Update_TelephoneNumber_TooLong_ShouldFail()
        {
            PersonPhone phone = GetPersonPhoneForEditing();

            var exception = Record.Exception(() => phone.UpdateTelephone("214-555-5555123145214514787771234785477777"));
            Assert.NotNull(exception);
        }

        private Address GetAddressForEditing()
            => Address.Create
            (
                0,
                "123 Main Street",
                "Ste 1",
                "Somewhereville",
                1,
                "12345"
            );

        private Person GetPersonForEditing()
            => Person.Create
            (
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

        private PersonPhone GetPersonPhoneForEditing()
            => PersonPhone.Create
                (
                    1,
                    PhoneNumberTypeEnum.Cell,
                    "972-564-1234"
                );
    }
}