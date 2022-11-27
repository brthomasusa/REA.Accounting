using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class PostalCode : ValueObject
    {
        public string? Value { get; }

        protected PostalCode() { }

        private PostalCode(string postalCode)
            : this() => Value = postalCode;

        public static implicit operator string(PostalCode self) => self.Value!;

        public static PostalCode Create(string postalCode)
        {
            CheckValidity(postalCode);
            return new PostalCode(postalCode);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 15)
            {
                throw new ArgumentException("Postal code can not be null or greater than 15 characters.");
            }
        }
    }
}