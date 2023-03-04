#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Guards;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Shared
{
    public sealed class PersonPhone : Entity<int>
    {
        private PersonPhone
        (
            int id,
            PhoneNumberTypeEnum phoneType,
            Telephone phoneNumber
        )
        {
            Id = id;
            PhoneNumberType = phoneType;
            Telephone = phoneNumber.Value!;
        }

        internal static Result<PersonPhone> Create
        (
            int id,
            PhoneNumberTypeEnum phoneNumberType,
            string telephone
        )
        {
            try
            {
                PersonPhone phone = new
                (
                    id,
                    Enum.IsDefined(typeof(PhoneNumberTypeEnum), phoneNumberType) ? phoneNumberType : throw new ArgumentException("Invalid phone number type."),
                    SharedKernel.CommonValueObjects.Telephone.Create(telephone)
                );

                return phone;
            }
            catch (Exception ex)
            {
                return Result<PersonPhone>.Failure<PersonPhone>(new Error("PersonPhone.Create", Helpers.GetExceptionMessage(ex)));
            }
        }

        public PhoneNumberTypeEnum PhoneNumberType { get; }

        public string Telephone { get; }
    }

    public enum PhoneNumberTypeEnum : int
    {
        Cell = 1,
        Home = 2,
        Work = 3
    }
}