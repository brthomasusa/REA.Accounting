using System.Text.RegularExpressions;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string? Value { get; }

        protected PhoneNumber() { }

        private PhoneNumber(string phoneNumber)
            : this()
        {
            Value = phoneNumber;
        }

        public static implicit operator string(PhoneNumber self) => self.Value!;

        public static PhoneNumber Create(string phoneNumber)
        {
            CheckValidity(phoneNumber);
            return new PhoneNumber(phoneNumber);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The PhoneNumber number is required.");
            }

            if (value.Length > 25)
            {
                throw new ArgumentException("Invalid PhoneNumber number, maximum length is 25 characters.");
            }

            Regex validatePhoneNumberRegex = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
            if (!validatePhoneNumberRegex.IsMatch(value))
                throw new ArgumentException($"{value} is not a valid phone number.");
        }
    }
}