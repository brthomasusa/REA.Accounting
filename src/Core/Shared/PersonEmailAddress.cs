#pragma warning disable CS8618

using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.Guards;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;

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

        internal static OperationResult<PersonEmailAddress> Create(int id, int emailAddressID, string email)
        {
            try
            {
                PersonEmailAddress emailAddress = new
                (
                    Guard.Against.LessThanZero(id, "BusinessEntityID", "BusinessEntity Id can not be negative."),
                    Guard.Against.LessThanZero(emailAddressID, "EmailAddressID", "Email address Id can not be negative."),
                    REA.Accounting.SharedKernel.CommonValueObjects.EmailAddress.Create(email)
                );

                return OperationResult<PersonEmailAddress>.CreateSuccessResult(emailAddress);
            }
            catch (Exception ex)
            {
                return OperationResult<PersonEmailAddress>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
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