#pragma warning disable CS8618

using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;

using ValueObject = REA.Accounting.Core.Shared.ValueObjects;

namespace REA.Accounting.Core.Shared
{
    public abstract class Person : Entity<int>
    {
        private readonly List<Address> _addresses = new();
        private readonly List<PersonEmailAddress> _emailAddresses = new();
        private readonly List<PersonPhone> _telephones = new();

        protected Person() { }

        protected Person
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

        protected Result<Person> UpdatePerson
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
                PersonName name = PersonName.Create(lastName, firstName, middleName);
                FirstName = name.FirstName!;
                LastName = name.LastName!;
                MiddleName = name.MiddleName!;
                Suffix = ValueObject.Suffix.Create(suffix).Value!;
                if (Enum.IsDefined(typeof(EmailPromotionEnum), emailPromotionEnum))
                {
                    EmailPromotions = emailPromotionEnum;
                }
                else
                {
                    throw new ArgumentException("Invalid email promotion flag");
                }

                UpdateModifiedDate();
                return this;
            }
            catch (Exception ex)
            {
                return Result<Person>.Failure<Person>(new Error("Person.UpdatePerson", Helpers.GetExceptionMessage(ex)));
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

        public Result<Address> AddAddress
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
                    return Result<Address>.Failure<Address>(new Error("Person.AddAddress", "There is already an address with this Id."));
                }

                Result<Address> result = Address.Create
                (
                    addressID, businessEntityID, addressType, line1, line2, city, stateProvinceID, postalCode
                );

                if (result.IsFailure)
                    return Result<Address>.Failure<Address>(new Error("Address.Create", result.Error.Message));

                _addresses.Add(result.Value);
                return result.Value;
            }
            catch (Exception ex)
            {
                return Result<Address>.Failure<Address>(new Error("Person.AddAddress", Helpers.GetExceptionMessage(ex)));
            }
        }

        public Result<Address> UpdateAddress
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
                    return Result<Address>.Failure<Address>(new Error("Person.AddAddress", $"Unable to locate an address with this Id {addressID}-{businessEntityID}."));
                }

                var address = _addresses.Find(addr => addr.Id == addressID && addr.BusinessEntityID == businessEntityID);
                Result<Address> result = address!.Update
                (
                    addressType, line1, line2, city, stateProvinceID, postalCode
                );

                if (result.IsFailure)
                    return Result<Address>.Failure<Address>(new Error("Person.UpdateAddress", result.Error.Message));

                return address;
            }
            catch (Exception ex)
            {
                return Result<Address>.Failure<Address>(new Error("Person.UpdateAddress", Helpers.GetExceptionMessage(ex)));
            }
        }

        public virtual IReadOnlyCollection<PersonEmailAddress> EmailAddresses => _emailAddresses.AsReadOnly();

        public Result<PersonEmailAddress> AddEmailAddress(int id, int emailAddressId, string emailAddress)
        {
            try
            {
                var searchResult = _emailAddresses.Find(mail => mail.Id == id && mail.EmailAddressID == emailAddressId);
                if (searchResult is not null)
                    return Result<PersonEmailAddress>.Failure<PersonEmailAddress>(new Error("Person.AddEmailAddress", "This is a duplicate email address."));

                Result<PersonEmailAddress> result = PersonEmailAddress.Create(id, emailAddressId, emailAddress);

                if (result.IsFailure)
                    return Result<PersonEmailAddress>.Failure<PersonEmailAddress>(new Error("Person.AddEmailAddress", result.Error.Message));

                return result.Value;
            }
            catch (Exception ex)
            {
                return Result<PersonEmailAddress>.Failure<PersonEmailAddress>(new Error("Person.AddEmailAddress", Helpers.GetExceptionMessage(ex)));
            }
        }

        public virtual IReadOnlyCollection<PersonPhone> Telephones => _telephones.AsReadOnly();

        public Result<PersonPhone> AddPhoneNumber(int id, PhoneNumberTypeEnum phoneType, string phoneNumber)
        {
            try
            {
                var searchResult = _telephones.Find(tel => tel.Id == id &&
                                                           tel.PhoneNumberType == phoneType &&
                                                           tel.Telephone == phoneNumber);

                if (searchResult is not null)
                    return Result<PersonPhone>.Failure<PersonPhone>(new Error("PersonPhone.AddPhoneNumber", "This is a duplicate phone number."));

                Result<PersonPhone> result = PersonPhone.Create(id, phoneType, phoneNumber);

                if (result.IsFailure)
                    return Result<PersonPhone>.Failure<PersonPhone>(new Error("PersonPhone.AddPhoneNumber", result.Error.Message));

                return result.Value;
            }
            catch (Exception ex)
            {
                return Result<PersonPhone>.Failure<PersonPhone>(new Error("", Helpers.GetExceptionMessage(ex)));
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