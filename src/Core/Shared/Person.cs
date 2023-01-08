#pragma warning disable CS8618

using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;

using ValueObject = REA.Accounting.Core.Shared.ValueObjects;

namespace REA.Accounting.Core.Shared
{
    public abstract class Person : Entity<int>
    {
        private List<Address> _addresses = new();
        private List<PersonEmailAddress> _emailAddresses = new();
        private List<PersonPhone> _telephones = new();

        protected Person() { }

        public Person
        (
            int personID,
            PersonType personType,
            NameStyleEnum nameStyle,
            Title title,
            PersonName name,
            Suffix suffix,
            EmailPromotionEnum emailPromotionEnum

        ) : this()
        {
            Id = personID;
            PersonType = personType.Value!;
            NameStyle = nameStyle;
            Title = title.Value!;
            FirstName = name.FirstName!;
            MiddleName = name.MiddleName!;
            LastName = name.LastName!;
            Suffix = suffix.Value!;
            EmailPromotions = emailPromotionEnum;
        }

        protected OperationResult<Person> UpdatePerson
        (
            string personType,
            NameStyleEnum nameStyle,
            string title,
            string firstName,
            string lastName,
            string middleName,
            string suffix,
            EmailPromotionEnum emailPromotionEnum
        )
        {
            try
            {
                PersonType = ValueObject.PersonType.Create(personType).Value!;
                NameStyle = Enum.IsDefined(typeof(NameStyleEnum), nameStyle) ? nameStyle : throw new ArgumentException("Invalid names style");
                Title = ValueObject.Title.Create(title).Value!;
                PersonName name = PersonName.Create(LastName, firstName, MiddleName);
                FirstName = name.FirstName!;
                LastName = name.LastName!;
                MiddleName = name.MiddleName!;
                Suffix = ValueObject.Suffix.Create(suffix).Value!;
                if (Enum.IsDefined(typeof(EmailPromotionEnum), emailPromotionEnum))
                {
                    EmailPromotions = emailPromotionEnum;
                    UpdateModifiedDate();
                }
                else
                {
                    throw new ArgumentException("Invalid email promotion flag");
                }

                UpdateModifiedDate();
                return OperationResult<Person>.CreateSuccessResult(this);
            }
            catch (Exception ex)
            {
                return OperationResult<Person>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public string PersonType { get; private set; }

        public NameStyleEnum NameStyle { get; private set; }

        public string Title { get; private set; }

        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public string Suffix { get; private set; }

        public EmailPromotionEnum EmailPromotions { get; private set; }

        public virtual IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

        public OperationResult<Address> AddAddress
        (
            int addressID,
            int businessEntityID,
            AddressTypeEnum addressType,
            string line1,
            string? line2,
            string city,
            int stateProvinceID,
            string postalCode
        )
        {
            try
            {
                if (_addresses.Find(addr => addr.Id == addressID) is not null)
                {
                    return OperationResult<Address>.CreateFailure("There is already an address with this Id.");
                }

                OperationResult<Address> result = Address.Create
                (
                    addressID, businessEntityID, addressType, line1, line2, city, stateProvinceID, postalCode
                );

                if (result.Success)
                {
                    _addresses.Add(result.Result);
                    return OperationResult<Address>.CreateSuccessResult(result.Result);
                }
                else
                {
                    return OperationResult<Address>.CreateFailure(result.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Address>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public OperationResult<Address> UpdateAddress
        (
            int addressID,
            int businessEntityID,
            AddressTypeEnum addressType,
            string line1,
            string? line2,
            string city,
            int stateProvinceID,
            string postalCode
        )
        {
            try
            {
                if (_addresses.Find(addr => addr.Id == addressID && addr.BusinessEntityID == businessEntityID) is null)
                {
                    return OperationResult<Address>.CreateFailure($"Unable to locate an address with this Id {addressID}-{businessEntityID}.");
                }

                var address = _addresses.Find(addr => addr.Id == addressID && addr.BusinessEntityID == businessEntityID);
                OperationResult<Address> result = address!.Update
                (
                    addressType, line1, line2, city, stateProvinceID, postalCode
                );

                if (result.Success)
                {
                    return OperationResult<Address>.CreateSuccessResult(address);
                }
                else
                {
                    return OperationResult<Address>.CreateFailure(result.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Address>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public virtual IReadOnlyCollection<PersonEmailAddress> EmailAddresses => _emailAddresses.AsReadOnly();

        public OperationResult<PersonEmailAddress> AddEmailAddress(int id, int emailAddressId, string emailAddress)
        {
            try
            {
                var searchResult = _emailAddresses.Find(mail => mail.Id == id && mail.EmailAddressID == emailAddressId);
                if (searchResult is not null)
                    return OperationResult<PersonEmailAddress>.CreateFailure("This is a duplicate email address.");

                OperationResult<PersonEmailAddress> result = PersonEmailAddress.Create(id, emailAddressId, emailAddress);
                if (result.Success)
                {
                    _emailAddresses.Add(result.Result);
                    return OperationResult<PersonEmailAddress>.CreateSuccessResult(result.Result);
                }
                else
                {
                    return OperationResult<PersonEmailAddress>.CreateFailure(result.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<PersonEmailAddress>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public virtual IReadOnlyCollection<PersonPhone> Telephones => _telephones.AsReadOnly();

        public OperationResult<PersonPhone> AddPhoneNumbers(int id, PhoneNumberTypeEnum phoneType, string phoneNumber)
        {
            try
            {
                var searchResult = _telephones.Find(tel => tel.Id == id &&
                                                           tel.PhoneNumberType == phoneType &&
                                                           tel.Telephone == phoneNumber);

                if (searchResult is not null)
                    return OperationResult<PersonPhone>.CreateFailure("This is a duplicate phone number.");


                OperationResult<PersonPhone> result = PersonPhone.Create(id, phoneType, phoneNumber);
                if (result.Success)
                {
                    _telephones.Add(result.Result);
                    return OperationResult<PersonPhone>.CreateSuccessResult(result.Result);
                }
                else
                {
                    return OperationResult<PersonPhone>.CreateFailure(result.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<PersonPhone>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }

    public enum NameStyleEnum : int
    {
        Western = 0,
        Eastern = 1
    }

    public enum EmailPromotionEnum : int
    {
        None = 0,
        FromAdventureWorksOnly = 1,
        FromAdventureWorksAndSelectPartners = 2
    }
}