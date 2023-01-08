using REA.Accounting.SharedKernel.Guards;

namespace REA.Accounting.SharedKernel.CommonValueObjects
{
    public class OrganizationName : ValueObject
    {
        public string? Value { get; }

        private OrganizationName(string orgName)
            => Value = orgName;

        public static implicit operator string(OrganizationName self) => self.Value!;

        public static OrganizationName Create(string orgName)
        {
            CheckValidity(orgName);
            return new OrganizationName(orgName);
        }

        private static void CheckValidity(string value)
        {
            Guard.Against.NullOrEmpty(value, "OrganizationName", "An organization name is required.");
            Guard.Against.LengthGreaterThan(value, 50, "OrganizationName", "Maximum length of the organization name is 50 characters.");
        }
    }
}