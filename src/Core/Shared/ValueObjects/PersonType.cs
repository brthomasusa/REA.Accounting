using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public class PersonType : ValueObject
    {
        private static readonly string[] _contactTypes = { "SC", "IN", "SP", "EM", "VC", "GC" };
        public string? Value { get; }

        protected PersonType() { }

        private PersonType(string contactType)
            : this() => Value = contactType;

        public static implicit operator string(PersonType self) => self.Value!;

        public static PersonType Create(string value)
        {
            CheckValidity(value);
            return new PersonType(value);
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