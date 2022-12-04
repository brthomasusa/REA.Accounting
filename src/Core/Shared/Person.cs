#pragma warning disable CS8618

using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

using ValueObject = REA.Accounting.Core.Shared.ValueObjects;

namespace REA.Accounting.Core.Shared
{
    public class Person : Entity<int>
    {
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

        public static Person Create
        (
            int personID,
            string personType,
            NameStyleEnum nameStyle,
            string title,
            string firstName,
            string? middleName,
            string lastName,
            string? suffix,
            EmailPromotionEnum emailPromotionEnum
        )
        {
            return new Person
            (
            personID,
            ValueObject.PersonType.Create(personType),
            Enum.IsDefined(typeof(NameStyleEnum), nameStyle) ? nameStyle : throw new ArgumentException("Invalid names style"),
            ValueObject.Title.Create(title),
            PersonName.Create(lastName, firstName, middleName!),
            ValueObject.Suffix.Create(suffix!),
            Enum.IsDefined(typeof(EmailPromotionEnum), emailPromotionEnum) ? emailPromotionEnum : throw new ArgumentException("Invalid email promotion flag")
            );
        }

        public string PersonType { get; private set; }
        public void UpdatePersonType(string value)
        {
            PersonType = ValueObject.PersonType.Create(value).Value!;
            UpdateLastModifiedDate();
        }

        public NameStyleEnum NameStyle { get; private set; }
        public void UpdateNameStyle(NameStyleEnum value)
        {
            NameStyle = Enum.IsDefined(typeof(NameStyleEnum), value) ? value : throw new ArgumentException("Invalid names style");
            UpdateLastModifiedDate();
        }

        public string Title { get; private set; }
        public void UpdateTitle(string value)
        {
            Title = ValueObject.Title.Create(value).Value!;
            UpdateLastModifiedDate();
        }

        public string FirstName { get; private set; }
        public void UpdateFirstName(string value)
        {
            PersonName name = PersonName.Create(LastName, value, MiddleName);
            FirstName = name.FirstName!;
            UpdateLastModifiedDate();
        }

        public string MiddleName { get; private set; }
        public void UpdateMiddleName(string value)
        {
            PersonName name = PersonName.Create(LastName, FirstName, value);
            MiddleName = name.MiddleName!;
            UpdateLastModifiedDate();
        }

        public string LastName { get; private set; }
        public void UpdateLastName(string value)
        {
            PersonName name = PersonName.Create(value, FirstName, MiddleName);
            LastName = name.LastName!;
            UpdateLastModifiedDate();
        }

        public string Suffix { get; private set; }
        public void UpdateSuffix(string value)
        {
            Suffix = ValueObject.Suffix.Create(value).Value!;
            UpdateLastModifiedDate();
        }

        public EmailPromotionEnum EmailPromotions { get; private set; }
        public void UpdateEmailPromotions(EmailPromotionEnum value)
        {
            if (Enum.IsDefined(typeof(EmailPromotionEnum), value))
            {
                EmailPromotions = value;
                UpdateLastModifiedDate();
            }
            else
            {
                throw new ArgumentException("Invalid email promotion flag");
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