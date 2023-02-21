using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.Core.Shared.ValueObjects
{
    public sealed class PersonType : ValueObject
    {
        private static readonly string[] _contactTypes = { "SC", "IN", "SP", "EM", "VC", "GC" };
        public string? Value { get; }

        private PersonType(string contactType)
            => Value = contactType;

        public static implicit operator string(PersonType self) => self.Value!;

        public static PersonType Create(string value)
        {
            CheckValidity(value);
            return new PersonType(value);
        }

        private static void CheckValidity(string value)
        {
            Guard.Against.NullOrEmpty(value, "PersonType", "The person type is required.");

            if (!Array.Exists(_contactTypes, element => element == value.ToUpper()))
            {
                throw new ArgumentException("Invalid contact type!");
            }
        }
    }
}