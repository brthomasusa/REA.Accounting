#pragma warning disable CS8618

using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.Shared
{
    public class PersonEmailAddress : Entity<int>
    {
        // PersonID = BusinessEntityID
        // Id = EmailAddressID

        protected PersonEmailAddress() { }

        private PersonEmailAddress(int id, int personId, EmailAddress emailAddress)
            : this()
        {
            Id = id;
            PersonID = personId;
            EmailAddress = emailAddress.Value!;
        }

        public static PersonEmailAddress Create(int id, int personId, string email)
        {
            return new PersonEmailAddress
            (
                id,
                personId,
                REA.Accounting.SharedKernel.CommonValueObjects.EmailAddress.Create(email)
            );
        }

        public int PersonID { get; private set; }

        public string EmailAddress { get; private set; }
        public void UpdateEmailAddress(string email)
        {
            EmailAddress = REA.Accounting.SharedKernel.CommonValueObjects.EmailAddress.Create(email).Value!;
            UpdateLastModifiedDate();
        }
    }
}