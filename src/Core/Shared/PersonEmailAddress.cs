#pragma warning disable CS8618

using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.Shared
{
    public class PersonEmailAddress : Entity<int>
    {
        private PersonEmailAddress(int id, int emailAddressID, EmailAddress emailAddress)
        {
            Id = id;
            EmailAddressID = emailAddressID;
            EmailAddress = emailAddress.Value!;
        }

        internal static PersonEmailAddress Create(int id, int emailAddressID, string email)
        {
            return new PersonEmailAddress
            (
                id,
                emailAddressID,
                REA.Accounting.SharedKernel.CommonValueObjects.EmailAddress.Create(email)
            );
        }

        public int EmailAddressID { get; private set; }

        public string EmailAddress { get; private set; }
        public void UpdateEmailAddress(string email)
        {
            EmailAddress = REA.Accounting.SharedKernel.CommonValueObjects.EmailAddress.Create(email).Value!;
            UpdateModifiedDate();
        }
    }
}