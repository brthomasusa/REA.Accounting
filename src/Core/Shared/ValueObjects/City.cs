using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class City
    {
        public string? Value { get; }

        protected City() { }

        private City(string line)
            : this() => Value = line;

        public static implicit operator string(City self) => self.Value!;

        public static City Create(string city)
        {
            CheckValidity(city);
            return new City(city);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 30)
            {
                throw new ArgumentException("City name can not be null or greater than 30 characters.");
            }
        }
    }
}