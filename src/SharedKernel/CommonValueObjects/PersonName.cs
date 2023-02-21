using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public sealed class PersonName : ValueObject
    {
        private PersonName(string last, string first, string? mi)
        {
            FirstName = first;
            LastName = last;
            MiddleName = mi;
        }

        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? MiddleName { get; init; }

        public static PersonName Create(string last, string first, string mi)
        {
            CheckValidity(last, first, mi);
            return new PersonName(last, first, mi);
        }

        private static void CheckValidity(string last, string first, string mi)
        {
            Guard.Against.NullOrEmpty(last, "LastName", "A last name is required.");
            Guard.Against.LengthGreaterThan(last, 25, "LastName", "Maximum length of the last name is 25 characters.");

            Guard.Against.NullOrEmpty(first, "FirstName", "A first name is required.");
            Guard.Against.LengthGreaterThan(first, 25, "FirstName", "Maximum length of the first name is 25 characters.");

            if (!string.IsNullOrEmpty(mi))
            {
                Guard.Against.LengthGreaterThan(mi, 25, "MiddleName", "Maximum length of the middle name is 25 characters.");
            }
        }
    }
}