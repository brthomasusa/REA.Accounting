#pragma warning disable CS8618

using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.Core.Shared;
using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel;
using REA.Accounting.SharedKernel.CommonValueObjects;

using ValueObject = REA.Accounting.Core.HumanResources.ValueObjects;

namespace REA.Accounting.Core.HumanResources
{
    public class Employee : Person
    {
        protected Employee
        (
            int employeeID,
            PersonType personType,
            NameStyleEnum nameStyle,
            Title title,
            PersonName name,
            Suffix suffix,
            EmailPromotionEnum emailPromotionEnum,
            NationalID nationalID,
            Login login,
            OrganizationNode orgNode,
            JobTitle jobTitle,
            DateOfBirth birthDate,
            MaritalStatus maritalStatus,
            Gender gender,
            DateOfHire hireDate,
            bool salaried,
            Vacation vacation,
            SickLeave sickLeave,
            bool active

        ) : base(employeeID, personType, nameStyle, title, name, suffix, emailPromotionEnum)
        {
            NationalIDNumber = nationalID.Value!;
            LoginID = login.Value;
            OrganizationNode = orgNode.Value;
            JobTitle = jobTitle.Value;
            BirthDate = birthDate.Value;
            MaritalStatus = maritalStatus.Value;
            Gender = gender.Value;
            HireDate = hireDate.Value;
            IsSalaried = salaried;
            VacationHours = vacation.Value;
            SickLeaveHours = sickLeave.Value;
            IsActive = active;
        }

        public static Employee Create
        (
            int employeeID,
            PersonType personType,
            NameStyleEnum nameStyle,
            Title title,
            PersonName name,
            Suffix suffix,
            string nationalID,
            string login,
            string orgNode,
            string jobTitle,
            DateOnly birthDate,
            string maritalStatus,
            string gender,
            DateOnly hireDate,
            bool salaried,
            int vacation,
            int sickLeave,
            bool active
        )
        {
            return new Employee
            (
                employeeID,
                personType.Value!.ToUpper().Equals("EM") ? personType : throw new ArgumentException("Employee must be person type 'EM'."),
                nameStyle,
                title,
                name,
                suffix,
                EmailPromotionEnum.None,
                NationalID.Create(nationalID),
                Login.Create(login),
                ValueObject.OrganizationNode.Create(orgNode),
                ValueObject.JobTitle.Create(jobTitle),
                ValueObject.DateOfBirth.Create(birthDate),
                ValueObject.MaritalStatus.Create(maritalStatus),
                ValueObject.Gender.Create(gender),
                ValueObject.DateOfHire.Create(hireDate),
                salaried,
                Vacation.Create(vacation),
                SickLeave.Create(sickLeave),
                active
            );
        }

        public string NationalIDNumber { get; private set; }
        public void UpdateNationalIDNumber(string value)
        {
            NationalIDNumber = NationalID.Create(value).Value!;
            UpdateLastModifiedDate();
        }

        public string LoginID { get; private set; }
        public void UpdateLoginID(string value)
        {
            LoginID = Login.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public string OrganizationNode { get; private set; }
        public void UpdateOrganizationNode(string value)
        {
            OrganizationNode = ValueObject.OrganizationNode.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public string JobTitle { get; private set; }
        public void UpdateJobTitle(string value)
        {
            JobTitle = ValueObject.JobTitle.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public DateOnly BirthDate { get; private set; }
        public void UpdateBirthDate(DateOnly value)
        {
            BirthDate = ValueObject.DateOfBirth.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public string MaritalStatus { get; private set; }
        public void UpdateMaritalStatus(string value)
        {
            MaritalStatus = ValueObject.MaritalStatus.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public string Gender { get; private set; }
        public void UpdateGender(string value)
        {
            Gender = ValueObject.Gender.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public DateOnly HireDate { get; private set; }
        public void UpdateHireDate(DateOnly value)
        {
            HireDate = ValueObject.DateOfHire.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public bool IsSalaried { get; private set; }
        public void UpdateIsSalaried(bool value)
        {
            IsSalaried = value;
            UpdateLastModifiedDate();
        }

        public int VacationHours { get; private set; }
        public void UpdateVacationHours(int value)
        {
            VacationHours = ValueObject.Vacation.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public int SickLeaveHours { get; private set; }
        public void UpdateSickLeaveHours(int value)
        {
            SickLeaveHours = ValueObject.SickLeave.Create(value).Value;
            UpdateLastModifiedDate();
        }

        public bool IsActive { get; private set; }
        public void UpdateIsActive(bool value)
        {
            IsActive = value;
            UpdateLastModifiedDate();
        }
    }
}