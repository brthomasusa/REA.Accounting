#pragma warning disable CS8600

using Xunit;
using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.UnitTests.Shared
{
    public class SharedValueObject_Tests
    {
        [Fact]
        public void AddressLine1_Valid_ShouldSucceed()
        {
            string line1 = "123 Main Street";
            AddressLine1 result = AddressLine1.Create(line1);

            Assert.IsType<AddressLine1>(result);
            Assert.Equal(line1, result);
        }

        [Fact]
        public void AddressLine1_Invalid_TooLong_ShouldFail()
        {
            string line1 = "12345678901234567890123456789012345678901234567890123456789012";
            var exception = Record.Exception(() => AddressLine1.Create(line1));

            Assert.NotNull(exception);
        }

        [Fact]
        public void AddressLine1_Invalid_Null_ShouldFail()
        {
            string? line1 = null;

            var exception = Record.Exception(() => AddressLine1.Create(line1!));

            Assert.NotNull(exception);
        }

        [Fact]
        public void AddressLine2_Valid_ShouldSucceed()
        {
            string line = "123 Main Street";
            AddressLine2 result = AddressLine2.Create(line);

            Assert.IsType<AddressLine2>(result);
            Assert.Equal(line, result);
        }

        [Fact]
        public void AddressLine2_Valid_EmptyString_ShouldSucceed()
        {
            string line = string.Empty;
            AddressLine2 result = AddressLine2.Create(line);

            Assert.IsType<AddressLine2>(result);
            Assert.Equal(line, result);
        }

        [Fact]
        public void AddressLine2_Valid_Null_ShouldSucceed()
        {
            string? line = null;
            AddressLine2 result = AddressLine2.Create(line!);

            Assert.IsType<AddressLine2>(result);
            Assert.Equal(line, result);
        }

        [Fact]
        public void AddressLine2_Invalid_TooLong_ShouldFail()
        {
            string line = "12345678901234567890123456789012345678901234567890123456789012";

            var exception = Record.Exception(() => AddressLine2.Create(line));

            Assert.NotNull(exception);
        }

        [Fact]
        public void City_Valid_ShouldSucceed()
        {
            string city = "Dallas";
            City result = City.Create(city);

            Assert.IsType<City>(result);
            Assert.Equal(city, result);
        }

        [Fact]
        public void City_Invalid_null_ShouldFail()
        {
            string? city = null;

            var exception = Record.Exception(() => City.Create(city!));

            Assert.NotNull(exception);
        }

        [Fact]
        public void City_Invalid_TooLong_ShouldFail()
        {
            string city = "1234567890123456789012345678901";

            var exception = Record.Exception(() => City.Create(city!));

            Assert.NotNull(exception);
        }

        [Fact]
        public void PostalCode_ValidData_ShouldSucceed()
        {
            string postalCode = "123456";
            PostalCode result = PostalCode.Create(postalCode);

            Assert.IsType<PostalCode>(result);
            Assert.Equal(postalCode, result);
        }

        [Fact]
        public void PostalCode_Invalid_EmptyString_ShouldFail()
        {
            string postalCode = string.Empty;

            var exception = Record.Exception(() => PostalCode.Create(postalCode));

            Assert.NotNull(exception);
        }

        [Fact]
        public void PostalCode_Invalid_TooManyCharacters_ShouldFail()
        {
            string postalCode = "12345678901234567890";

            var exception = Record.Exception(() => PostalCode.Create(postalCode));
            Assert.NotNull(exception);
        }

        [Fact]
        public void ContactType_ValidData_ShouldSucceed()
        {
            string contactType = "SC";
            PersonType result = PersonType.Create(contactType);

            Assert.IsType<PersonType>(result);
            Assert.Equal(contactType, result);
        }

        [Fact]
        public void Contact_Invalid_Null_ShouldFail()
        {
            string contactType = null;

            var exception = Record.Exception(() => PersonType.Create(contactType!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Contact_Invalid_InvalidType_ShouldFail()
        {
            string contactType = "12";

            var exception = Record.Exception(() => PersonType.Create(contactType!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Title_ValidData_ShouldSucceed()
        {
            string title = "Miss";
            Title result = Title.Create(title);

            Assert.IsType<Title>(result);
            Assert.Equal(title, result);
        }

        [Fact]
        public void Title_ValidData_Null_ShouldSucceed()
        {
            string title = null;
            Title result = Title.Create(title!);

            Assert.IsType<Title>(result);
            Assert.Equal(title, result);
        }

        [Fact]
        public void Title_Invalid_TooLong_ShouldFail()
        {
            string title = "123456789";

            var exception = Record.Exception(() => Title.Create(title));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Suffix_ValidData_ShouldSucceed()
        {
            string suffix = "Junior";
            Suffix result = Suffix.Create(suffix!);

            Assert.IsType<Suffix>(result);
            Assert.Equal(suffix, result);
        }

        [Fact]
        public void Suffix_ValidData_Null_ShouldSucceed()
        {
            string suffix = null;
            Suffix result = Suffix.Create(suffix!);

            Assert.IsType<Suffix>(result);
            Assert.Equal(suffix, result);
        }

        [Fact]
        public void Suffix_Invalid_TooLong_ShouldFail()
        {
            string suffix = "12345678901";

            var exception = Record.Exception(() => Suffix.Create(suffix));
            Assert.NotNull(exception);
        }

        [Fact]
        public void EmailAddress_ValidData_ShouldSucceed()
        {
            string email = "some.one@example.com";
            EmailAddress result = EmailAddress.Create(email);

            Assert.IsType<EmailAddress>(result);
            Assert.Equal(email, result);
        }

        [Fact]
        public void EmailAddress_EmptyString_ShouldFail()
        {
            string email = string.Empty;

            var exception = Record.Exception(() => EmailAddress.Create(email));
            Assert.NotNull(exception);
        }

        [Fact]
        public void EmailAddress_Null_ShouldFail()
        {
            string email = null;

            var exception = Record.Exception(() => EmailAddress.Create(email!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void EmailAddress_Malformed1_ShouldFail()
        {
            string email = "hello@";

            var exception = Record.Exception(() => EmailAddress.Create(email!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void EmailAddress_Malformed2_ShouldFail()
        {
            string email = "@test";

            var exception = Record.Exception(() => EmailAddress.Create(email!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void EmailAddress_Malformed3_ShouldFail()
        {
            string email = "theproblem@test@gmail.com";

            var exception = Record.Exception(() => EmailAddress.Create(email!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void EmailAddress_Malformed4_ShouldFail()
        {
            string email = "mail with@space.com";

            var exception = Record.Exception(() => EmailAddress.Create(email!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void PhoneNumber_InternationalPhoneNumber_ShouldSucceed()
        {
            string phoneNumber = "1 (11) 500 555-0190";

            var exception = Record.Exception(() => PhoneNumber.Create(phoneNumber));
            Assert.Null(exception);
        }

        [Fact]
        public void PhoneNumber_USPhoneNumber_ShouldSucceed()
        {
            string phoneNumber = "214-555-5555";

            var exception = Record.Exception(() => PhoneNumber.Create(phoneNumber));
            Assert.Null(exception);
        }
    }
}