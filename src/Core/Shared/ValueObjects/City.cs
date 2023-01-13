using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class City : ValueObject
    {
        public string? Value { get; }

        private City(string line)
            => Value = line;

        public static implicit operator string(City self) => self.Value!;

        public static City Create(string city)
        {
            CheckValidity(city);
            return new City(city);
        }

        private static void CheckValidity(string value)
        {
            Guard.Against.NullOrEmpty(value, "City", "The city is required.");
            Guard.Against.LengthGreaterThan(value, 30, "Address Line 1", "The maximum length of the city name is 30 characters.");
        }
    }
}