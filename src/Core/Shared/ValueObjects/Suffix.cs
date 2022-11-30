using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class Suffix : ValueObject
    {
        public string? Value { get; }

        protected Suffix() { }

        private Suffix(string value)
            : this() => Value = value;

        public static implicit operator string(Suffix self) => self.Value!;

        public static Suffix Create(string value)
        {
            CheckValidity(value);
            return new Suffix(value);
        }

        private static void CheckValidity(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 10)
            {
                throw new ArgumentException("The suffix can not be greater than 10 characters.");
            }
        }
    }
}