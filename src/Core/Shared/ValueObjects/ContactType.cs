using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class ContactType : ValueObject
    {
        private static readonly string[] _contactTypes = { "SC", "IN", "SP", "EM", "VC", "GC" };
        public string? Value { get; }

        protected ContactType() { }

        private ContactType(string contactType)
            : this() => Value = contactType;

        public static implicit operator string(ContactType self) => self.Value!;

        public static ContactType Create(string value)
        {
            CheckValidity(value);
            return new ContactType(value);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The contact type is required.");
            }

            if (!Array.Exists(_contactTypes, element => element == value.ToUpper()))
            {
                throw new ArgumentException("Invalid contact type!");
            }
        }
    }
}