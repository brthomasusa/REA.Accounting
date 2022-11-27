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
    }
}