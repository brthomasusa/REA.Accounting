using System.Text.RegularExpressions;
using REA.Accounting.SharedKernel.Base;
namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class Address : ValueObject
    {
        public string? AddressLine1 { get; }
        public string? AddressLine2 { get; }
        public string? City { get; }
        public int StateProvinceID { get; }
        public string? Zipcode { get; }

        protected Address() { }

        private Address(string line1, string? line2, string city, int stateCode, string zipcode)
        {
            AddressLine1 = line1;
            AddressLine2 = line2;
            City = city;
            StateProvinceID = stateCode;
            Zipcode = zipcode;
        }

        public static Address Create(string line1, string? line2, string city, int stateCode, string zipcode)
        {
            CheckValidity(line1, line2, city, stateCode, zipcode);
            return new Address(line1, line2, city, stateCode, zipcode);
        }

        private static void CheckValidity(string line1, string? line2, string city, int stateCode, string zipcode)
        {
            if (string.IsNullOrEmpty(line1))
            {
                throw new ArgumentNullException(nameof(line1), "The first address line is required.");
            }

            if (line1.Length > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(line1), "Address line can not be longer than 30 characters.");
            }

            if (!string.IsNullOrEmpty(line2) && line2.Length > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(line2), "Address line can not be longer than 30 characters.");
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException(nameof(city), "A city name is required.");
            }

            if (city.Length > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(city), "City name can not be longer than 30 characters.");
            }

            //TODO Validate state codes against Person.StateProvince table
            if (stateCode <= 0)
            {
                throw new ArgumentNullException(nameof(stateCode), "A state/province code is required.");
            }

            //TODO Validate postal codes against a yet to be built postal code lookup service/table
            if (string.IsNullOrEmpty(zipcode))
            {
                throw new ArgumentNullException(nameof(zipcode), "A zip code is required.");
            }

            if (zipcode.Length > 15)
            {
                throw new ArgumentOutOfRangeException(nameof(zipcode), "Postal code can not be longer than 15 characters.");
            }
        }
    }
}