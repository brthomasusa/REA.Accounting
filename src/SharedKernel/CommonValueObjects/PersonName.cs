namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class PersonName : ValueObject
    {
        protected PersonName() { }

        private PersonName(string last, string first, string? mi)
            : this()
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

            if (first.Length > 50)
            {
                throw new ArgumentOutOfRangeException("Maximum length of the first name is 25 characters.", nameof(first));
            }

            if (last.Length > 50)
            {
                throw new ArgumentOutOfRangeException("Maximum length of the last name is 25 characters.", nameof(last));
            }

            if (!string.IsNullOrEmpty(mi))
            {
                mi = mi.Trim();
                if (mi.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("Maximum length of middle initial is 1 character.", nameof(mi));
                }
            }
        }
    }
}