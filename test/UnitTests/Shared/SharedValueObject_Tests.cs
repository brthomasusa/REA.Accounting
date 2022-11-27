using Xunit;
using REA.Accounting.Core.Shared.ValueObjects;

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

            Action action = () => AddressLine1.Create(line1);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Address line 1 can not be null or greater than 60 characters.", caughtException.Message);
        }

        [Fact]
        public void AddressLine1_Invalid_Null_ShouldFail()
        {
            string? line1 = null;

            Action action = () => AddressLine1.Create(line1!);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Address line 1 can not be null or greater than 60 characters.", caughtException.Message);
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

            Action action = () => AddressLine2.Create(line);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Address line 2 can not be greater than 60 characters.", caughtException.Message);
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

            Action action = () => City.Create(city!);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("City name can not be null or greater than 30 characters.", caughtException.Message);
        }

        [Fact]
        public void City_Invalid_TooLong_ShouldFail()
        {
            string city = "1234567890123456789012345678901";

            Action action = () => City.Create(city);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("City name can not be null or greater than 30 characters.", caughtException.Message);
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

            Action action = () => PostalCode.Create(postalCode);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Postal code can not be null or greater than 15 characters.", caughtException.Message);
        }

        [Fact]
        public void PostalCode_Invalid_TooManyCharacters_ShouldFail()
        {
            string postalCode = "12345678901234567890";

            Action action = () => PostalCode.Create(postalCode);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Postal code can not be null or greater than 15 characters.", caughtException.Message);
        }
    }
}