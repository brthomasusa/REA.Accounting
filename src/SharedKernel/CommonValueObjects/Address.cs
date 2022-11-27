using System.Text.RegularExpressions;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class Address : ValueObject
    {
        private static readonly string[] _stateCodes = { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "GA", "HI", "IA",
                                                         "ID", "IL", "IN", "KS", "KY", "LA", "MA", "ME", "MD", "MI", "MN", "MO",
                                                         "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK",
                                                         "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WI",
                                                         "WV", "WY" };

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
                throw new ArgumentNullException("The first address line is required.", nameof(line1));
            }

            if (line1.Length > 30)
            {
                throw new ArgumentOutOfRangeException("Address line can not be longer than 30 characters.", nameof(line1));
            }

            if (!string.IsNullOrEmpty(line2) && line2.Length > 30)
            {
                throw new ArgumentOutOfRangeException("Address line can not be longer than 30 characters.", nameof(line2));
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException("A city name is required.", nameof(city));
            }

            if (city.Length > 30)
            {
                throw new ArgumentOutOfRangeException("City name can not be longer than 30 characters.", nameof(city));
            }

            //TODO Validate state codes against Person.StateProvince table
            if (stateCode <= 0)
            {
                throw new ArgumentNullException("A state/province code is required.", nameof(stateCode));
            }

            //TODO Validate postal codes against a yet to be built postal code lookup service/table
            if (string.IsNullOrEmpty(zipcode))
            {
                throw new ArgumentNullException("A zip code is required.", nameof(zipcode));
            }

            if (zipcode.Length > 15)
            {
                throw new ArgumentOutOfRangeException("Postal code can not be longer than 15 characters.", nameof(zipcode));
            }
        }
    }
}