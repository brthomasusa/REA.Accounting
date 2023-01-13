using System.Text.RegularExpressions;
using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class NationalID : ValueObject
    {
        public string? Value { get; }

        protected NationalID() { }

        private NationalID(string idNumber)
            : this()
        {
            Value = idNumber;
        }

        public static implicit operator string(NationalID self) => self.Value!;

        public static NationalID Create(string idNumber)
        {
            CheckValidity(idNumber);
            return new NationalID(idNumber);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The national id number is required.");
            }

            if (value.Length > 15)
            {
                throw new ArgumentException("Invalid national id number, maximum length is 15 characters.");
            }

            Regex validateNationalIdNumberRegex = new Regex("^\\d{5,9}$");
            if (!validateNationalIdNumberRegex.IsMatch(value))
                throw new ArgumentException($"{value} is not a valid national id number, should be 5 - 9 digits.");
        }
    }
}