#pragma warning disable CS8618

using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.Core.Shared;
using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.SharedKernel.CommonValueObjects;

using ValueObject = REA.Accounting.Core.HumanResources.ValueObjects;

namespace REA.Accounting.Core.HumanResources
{
    public class Employee : Person
    {
        private List<DepartmentHistory> _deptHistories = new();
        private List<PayHistory> _payHistories = new();

        public Employee() { }

        public Employee
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
            JobTitle = jobTitle.Value;
            BirthDate = birthDate.Value;
            MaritalStatus = maritalStatus.Value;
            Gender = gender.Value;
            HireDate = hireDate.Value;
            IsSalaried = salaried;
            VacationHours = vacation.Value;
            SickLeaveHours = sickLeave.Value;
            IsActive = active;

            CheckValidity();
        }

        public static Employee Create
        (
            int employeeID,
            string personType,
            NameStyleEnum nameStyle,
            string title,
            string firstName,
            string lastName,
            string middleName,
            string suffix,
            string nationalID,
            string login,
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
                REA.Accounting.Core.Shared.ValueObjects.PersonType.Create(personType),
                nameStyle,
                REA.Accounting.Core.Shared.ValueObjects.Title.Create(title),
                PersonName.Create(lastName, firstName, middleName),
                REA.Accounting.Core.Shared.ValueObjects.Suffix.Create(suffix),
                EmailPromotionEnum.None,
                NationalID.Create(nationalID),
                Login.Create(login),
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

        public OperationResult<Employee> Update
        (
            string personType,
            NameStyleEnum nameStyle,
            string title,
            string firstName,
            string lastName,
            string middleName,
            string suffix,
            EmailPromotionEnum emailPromotionEnum,
            string nationalID,
            string login,
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
            try
            {

                base.UpdatePerson(personType, nameStyle, title, firstName, lastName, middleName, suffix, emailPromotionEnum);

                NationalIDNumber = NationalID.Create(nationalID).Value!;
                LoginID = Login.Create(login).Value;
                JobTitle = ValueObject.JobTitle.Create(jobTitle).Value;
                BirthDate = ValueObject.DateOfBirth.Create(birthDate).Value;
                MaritalStatus = ValueObject.MaritalStatus.Create(maritalStatus).Value;
                Gender = ValueObject.Gender.Create(gender).Value;
                HireDate = ValueObject.DateOfHire.Create(hireDate).Value;
                IsSalaried = salaried;
                VacationHours = ValueObject.Vacation.Create(vacation).Value;
                SickLeaveHours = ValueObject.SickLeave.Create(sickLeave).Value;
                IsActive = active;

                UpdateModifiedDate();
                return OperationResult<Employee>.CreateSuccessResult(this);
            }
            catch (Exception ex)
            {
                return OperationResult<Employee>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }

        public string NationalIDNumber { get; private set; }

        public string LoginID { get; private set; }

        public string OrganizationNode { get; private set; }

        public string JobTitle { get; private set; }

        public DateOnly BirthDate { get; private set; }

        public string MaritalStatus { get; private set; }

        public string Gender { get; private set; }

        public DateOnly HireDate { get; private set; }

        public bool IsSalaried { get; private set; }

        public int VacationHours { get; private set; }

        public int SickLeaveHours { get; private set; }

        public bool IsActive { get; private set; }

        public virtual IReadOnlyCollection<DepartmentHistory> DepartmentHistories => _deptHistories.ToList();
        public virtual IReadOnlyCollection<PayHistory> PayHistories => _payHistories.ToList();

        protected override void CheckValidity()
        {
            if (!PersonType.ToUpper().Equals("EM"))
                throw new ArgumentException("Employee must be person type 'EM'.");
        }
    }
}