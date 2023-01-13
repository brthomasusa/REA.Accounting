using System.Text.RegularExpressions;
using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.Organization
{
    public class EmployerIdentificationNumber : ValueObject
    {
        public string? Value { get; }

        protected EmployerIdentificationNumber() { }

        private EmployerIdentificationNumber(string ein)
            : this() => Value = ein;

        public static implicit operator string(EmployerIdentificationNumber self) => self.Value!;

        public static EmployerIdentificationNumber Create(string ein)
        {
            CheckValidity(ein);
            return new EmployerIdentificationNumber(ein);
        }

        private static void CheckValidity(string value)
        {                           // "^[1-9]\d?-\d{7}$"
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("employer identification number is require.");

            if (!Regex.IsMatch(value, @"^\d{9}|\d{2}-\d{7}$"))
                throw new ArgumentException($"Invalid employer identification number {value}!");
        }
    }
}