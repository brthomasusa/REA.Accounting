using System.Text.RegularExpressions;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class PointOfContact : ValueObject
    {
        protected PointOfContact() { }

        private PointOfContact(string fname, string lname, string? mi, string telephone)
        {
            FirstName = fname;
            LastName = lname;
            MiddleInitial = mi;
            Telephone = telephone;
        }

        public string? FirstName { get; }
        public string? LastName { get; }
        public string? MiddleInitial { get; }
        public string? Telephone { get; }

        public static PointOfContact Create(string fname, string lname, string? mi, string telephone)
        {
            CheckValidity(fname, lname, mi, telephone);
            return new PointOfContact(fname, lname, mi, telephone);
        }

        private static void CheckValidity(string last, string first, string? mi, string telephone)
        {
            if (string.IsNullOrEmpty(first))
            {
                throw new ArgumentNullException("A first name is required.", nameof(first));
            }

            if (string.IsNullOrEmpty(last))
            {
                throw new ArgumentNullException("A last name is required.", nameof(last));
            }

            first = first.Trim();
            last = last.Trim();

            if (first.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Maximum length of the first name is 25 characters.", nameof(first));
            }

            if (last.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Maximum length of the last name is 25 characters.", nameof(last));
            }

            if (!string.IsNullOrEmpty(mi))
            {
                mi = mi.Trim();
                if (mi.Length > 1)
                {
                    throw new ArgumentOutOfRangeException("Maximum length of middle initial is 1 character.", nameof(mi));
                }
            }

            string rgPhoneNumber = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

            if (string.IsNullOrEmpty(telephone))
            {
                throw new ArgumentNullException("The PhoneNumber number is required.", nameof(telephone));
            }

            if (!Regex.IsMatch(telephone, rgPhoneNumber))
            {
                throw new ArgumentException("Invalid PhoneNumber number!", nameof(telephone));
            }
        }
    }
}