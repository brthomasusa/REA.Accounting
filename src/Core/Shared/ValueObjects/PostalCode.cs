using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public sealed class PostalCode : ValueObject
    {
        public string? Value { get; }

        private PostalCode(string postalCode)
            => Value = postalCode;

        public static implicit operator string(PostalCode self) => self.Value!;

        public static PostalCode Create(string postalCode)
        {
            CheckValidity(postalCode);
            return new PostalCode(postalCode);
        }

        private static void CheckValidity(string value)
        {
            Guard.Against.NullOrEmpty(value, "PostalCode", "The postal code is required.");
            Guard.Against.LengthGreaterThan(value, 15, "PostalCode", "The maximum length of the postal code is 15 characters.");
        }
    }
}