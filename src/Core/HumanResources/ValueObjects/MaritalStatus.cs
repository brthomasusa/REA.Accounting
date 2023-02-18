#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class MaritalStatus : ValueObject
    {
        public string Value { get; }

        protected MaritalStatus() { }

        private MaritalStatus(string value) : this() => Value = value.ToUpper();

        public static implicit operator string(MaritalStatus self) => self.Value;

        public static MaritalStatus Create(string status)
        {
            CheckValidity(status);
            return new MaritalStatus(status);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value), "The marital status is required.");
            }

            if (!string.Equals(value, "M", StringComparison.OrdinalIgnoreCase) && !string.Equals(value, "S", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid marital status, valid statues are 'S' and 'M'.", nameof(value));
            }
        }
    }
}