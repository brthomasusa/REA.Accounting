using System.Globalization;
using System.Text.RegularExpressions;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class EmailAddress : ValueObject
    {
        public string? Value { get; }

        private EmailAddress(string email)
            => Value = email;

        public static implicit operator string(EmailAddress self) => self.Value!;

        public static EmailAddress Create(string value)
        {
            CheckValidity(value);
            return new EmailAddress(value);
        }

        private static void CheckValidity(string value)
        {
            Guard.Against.NullOrEmpty(value, "EmailAddress", "The email address is required.");
            Guard.Against.LengthGreaterThan(value, 50, "EmailAddress", "The maximum length of the email address is 50 characters.");

            if (!IsValidEmail(value))
            {
                throw new ArgumentException("Invalid email addresss.", nameof(value));
            }
        }

        // Credit where credit due; thank you Microsoft! 
        // https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}