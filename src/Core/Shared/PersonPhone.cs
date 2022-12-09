#pragma warning disable CS8618

using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.Core.Shared
{
    public class PersonPhone : Entity<int>
    {
        protected PersonPhone() { }

        private PersonPhone
        (
            int id,
            PhoneNumberTypeEnum phoneType,
            PhoneNumber phoneNumber
        ) : this()
        {
            Id = id;
            PhoneNumberType = phoneType;
            Telephone = phoneNumber.Value!;
        }

        public static PersonPhone Create
        (
            int id,
            PhoneNumberTypeEnum phoneNumberType,
            string telephone
        )
        {
            return new PersonPhone
            (
                id,
                Enum.IsDefined(typeof(PhoneNumberTypeEnum), phoneNumberType) ? phoneNumberType : throw new ArgumentException("Invalid phone number type."),
                PhoneNumber.Create(telephone)
            );
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