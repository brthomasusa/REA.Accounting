using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class AddressLine2 : ValueObject
    {
        public string? Value { get; }

        protected AddressLine2() { }

        private AddressLine2(string line)
            : this() => Value = line;

        public static implicit operator string(AddressLine2 self) => self.Value!;

        public static AddressLine2 Create(string line)
        {
            CheckValidity(line);
            return new AddressLine2(line);
        }

        private static void CheckValidity(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 60)
            {
                throw new ArgumentException("Address line 2 can not be greater than 60 characters.");
            }
        }
    }
}