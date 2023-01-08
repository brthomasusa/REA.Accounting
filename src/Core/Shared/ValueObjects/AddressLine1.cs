using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class AddressLine1 : ValueObject
    {
        public string? Value { get; }

        private AddressLine1(string line)
            => Value = line;

        public static implicit operator string(AddressLine1 self) => self.Value!;

        public static AddressLine1 Create(string line)
        {
            CheckValidity(line);
            return new AddressLine1(line);
        }

        private static void CheckValidity(string value)
        {
            Guard.Against.NullOrEmpty(value, "Address Line 1", "The first address line is required.");
            Guard.Against.LengthGreaterThan(value, 60, "Address Line 1", "The maximum length of an address line is 60 characters.");
        }
    }
}