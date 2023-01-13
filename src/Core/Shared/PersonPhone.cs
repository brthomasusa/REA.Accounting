#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Guards;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Shared
{
    public class PersonPhone : Entity<int>
    {
        private PersonPhone
        (
            int id,
            PhoneNumberTypeEnum phoneType,
            PhoneNumber phoneNumber
        )
        {
            Id = id;
            PhoneNumberType = phoneType;
            Telephone = phoneNumber.Value!;
        }

        internal static OperationResult<PersonPhone> Create
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
                    PhoneNumber.Create(telephone)
                );

                return OperationResult<PersonPhone>.CreateSuccessResult(phone);

            }
            catch (Exception ex)
            {
                return OperationResult<PersonPhone>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public PhoneNumberTypeEnum PhoneNumberType { get; private set; }
        public void UpdatePhoneNumberType(PhoneNumberTypeEnum value)
        {
            PhoneNumberType = Enum.IsDefined(typeof(PhoneNumberTypeEnum), value) ? value : throw new ArgumentException("Invalid phone number type.");
            UpdateModifiedDate();
        }

        public string Telephone { get; private set; }
        public void UpdateTelephone(string value)
        {
            PhoneNumber phoneNumber = PhoneNumber.Create(value);
            Telephone = phoneNumber.Value!;
            UpdateModifiedDate();
        }
    }

    public enum PhoneNumberTypeEnum : int
    {
        Cell = 1,
        Home = 2,
        Work = 3
    }
}