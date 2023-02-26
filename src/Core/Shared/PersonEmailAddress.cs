#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Shared
{
    public sealed class PersonEmailAddress : Entity<int>
    {
        private PersonEmailAddress(int id, int emailAddressID, EmailAddress emailAddress)
        {
            Id = id;
            EmailAddressID = emailAddressID;
            EmailAddress = emailAddress.Value!;
        }

        internal static Result<PersonEmailAddress> Create(int id, int emailAddressID, string email)
        {
            try
            {
                PersonEmailAddress emailAddress = new
                (
                    Guard.Against.LessThanZero(id, "BusinessEntityID", "BusinessEntity Id can not be negative."),
                    Guard.Against.LessThanZero(emailAddressID, "EmailAddressID", "Email address Id can not be negative."),
                    REA.Accounting.SharedKernel.CommonValueObjects.EmailAddress.Create(email)
                );

                return emailAddress;
            }
            catch (Exception ex)
            {
                return Result<PersonEmailAddress>.Failure<PersonEmailAddress>(new Error("PersonEmailAddress.Create", Helpers.GetExceptionMessage(ex)));
            }
        }

        public int EmailAddressID { get; }

        public string EmailAddress { get; private set; }
        public void UpdateEmailAddress(string email)
        {
            EmailAddress = REA.Accounting.SharedKernel.CommonValueObjects.EmailAddress.Create(email).Value!;
            UpdateModifiedDate();
        }
    }
}