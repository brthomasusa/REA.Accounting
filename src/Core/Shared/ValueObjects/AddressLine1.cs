using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class AddressLine1 : ValueObject
    {
        public string? Value { get; }

        protected AddressLine1() { }

        private AddressLine1(string line)
            : this() => Value = line;

        public static implicit operator string(AddressLine1 self) => self.Value!;

        public static AddressLine1 Create(string line)
        {
            CheckValidity(line);
            return new AddressLine1(line);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 60)
            {
                throw new ArgumentException("Address line 1 can not be null or greater than 60 characters.");
            }
        }
    }
}