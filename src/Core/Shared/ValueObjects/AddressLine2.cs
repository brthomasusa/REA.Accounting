using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public sealed class AddressLine2 : ValueObject
    {
        public string? Value { get; }

        private AddressLine2(string line)
            => Value = line;

        public static implicit operator string(AddressLine2 self) => self.Value!;

        public static AddressLine2 Create(string line)
        {
            CheckValidity(line);
            return new AddressLine2(line);
        }

        private static void CheckValidity(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Guard.Against.LengthGreaterThan(value, 60, "Address Line 2", "The maximum length of an address line is 60 characters.");
            }
        }
    }
}