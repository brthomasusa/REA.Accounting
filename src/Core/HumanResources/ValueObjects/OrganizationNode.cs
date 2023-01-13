#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;

namespace REA.Accounting.Core.HumanResources.ValueObjects
{
    public class OrganizationNode : ValueObject
    {
        public string Value { get; }

        protected OrganizationNode() { }

        private OrganizationNode(string value) : this()
            => Value = value;

        public static implicit operator string(OrganizationNode self) => self.Value;

        public static OrganizationNode Create(string orgNode)
        {
            CheckValidity(orgNode);
            return new OrganizationNode(orgNode);
        }

        private static void CheckValidity(string value)
        {
            if (value.Length > 50)
            {
                throw new ArgumentException("Invalid organization node, maximum length is 50 characters.");
            }
        }
    }
}