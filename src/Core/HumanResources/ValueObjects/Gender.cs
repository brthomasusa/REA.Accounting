#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class Gender : ValueObject
    {
        public string Value { get; }

        protected Gender() { }

        private Gender(string value) : this()
            => Value = value.ToUpper();

        public static implicit operator string(Gender self) => self.Value;

        public static Gender Create(string gender)
        {
            CheckValidity(gender);
            return new Gender(gender);
        }

        private static void CheckValidity(string gender)
        {
            if (string.IsNullOrEmpty(gender))
            {
                throw new ArgumentNullException(nameof(gender), "Gender is required.");
            }

            if (!string.Equals(gender, "M", StringComparison.OrdinalIgnoreCase) && !string.Equals(gender, "F", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid gender, valid statues are 'M' for male and 'F' for female.");
            }
        }
    }
}