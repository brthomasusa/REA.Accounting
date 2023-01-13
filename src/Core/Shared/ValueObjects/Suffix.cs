using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class Suffix : ValueObject
    {
        public string? Value { get; }

        private Suffix(string value)
            => Value = value;

        public static implicit operator string(Suffix self) => self.Value!;

        public static Suffix Create(string value)
        {
            CheckValidity(value);
            return new Suffix(value);
        }

        private static void CheckValidity(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Guard.Against.LengthGreaterThan(value, 10, "Suffix", "The maximum length of the suffix is 10 characters.");
            }
        }
    }
}