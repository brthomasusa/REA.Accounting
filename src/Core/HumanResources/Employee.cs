#pragma warning disable CS8618

using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.Core.Shared;
using REA.Accounting.Core.Shared.ValueObjects;
using REA.Accounting.SharedKernel.CommonValueObjects;
using REA.Accounting.SharedKernel.Utilities;
using SharedValueObject = REA.Accounting.Core.Shared.ValueObjects;
using ValueObject = REA.Accounting.Core.HumanResources.ValueObjects;

namespace REA.Accounting.Core.HumanResources
{
    public class Employee : Person
    {
        private readonly List<DepartmentHistory> _deptHistories = new();
        private readonly List<PayHistory> _payHistories = new();

        protected Employee
        (
            int employeeID,
            PersonType personType,
            NameStyleEnum nameStyle,
            Title? title,
            PersonName name,
            Suffix? suffix,
            EmailPromotionEnum emailPromotionEnum,
            int managerID,
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

        ) : base(employeeID, personType, nameStyle, title!, name, suffix!, emailPromotionEnum)
        {
            ManagerID = managerID;
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

        public static Result<Employee> Create
        (
            int employeeID,
            string personType,
            NameStyleEnum nameStyle,
            string? title,
            string firstName,
            string lastName,
            string middleName,
            string? suffix,
            int managerID,
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
                Employee employee = new(
                    employeeID,

                    SharedValueObject.PersonType.Create(personType),
                    nameStyle,
                    SharedValueObject.Title.Create(title!),
                    PersonName.Create(lastName, firstName, middleName),
                    SharedValueObject.Suffix.Create(suffix!),
                    EmailPromotionEnum.None,
                    managerID,
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

                return employee;
            }
            catch (Exception ex)
            {
                return Result<Employee>.Failure<Employee>(new Error("Employee.Create", Helpers.GetExceptionMessage(ex)));
            }
        }

        public Result<Employee> Update
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
                Result<Person> result = base.UpdatePerson(personType, nameStyle, title, firstName, lastName, middleName, suffix, emailPromotionEnum);

                if (result.IsFailure)
                    return Result<Employee>.Failure<Employee>(new Error("", result.Error.Message));

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

                CheckValidity();
                UpdateModifiedDate();
                return this;
            }
            catch (Exception ex)
            {
                return Result<Employee>.Failure<Employee>(new Error("Employee.Update", Helpers.GetExceptionMessage(ex)));
            }
        }

        public int ManagerID { get; }
        public string NationalIDNumber { get; private set; }

        public string LoginID { get; private set; }

        public string OrganizationNode { get; }

        public string JobTitle { get; private set; }

        public DateOnly BirthDate { get; private set; }

        public string MaritalStatus { get; private set; }

        public string Gender { get; private set; }

        public DateOnly HireDate { get; private set; }

        public bool IsSalaried { get; private set; }

        public int VacationHours { get; private set; }

        public int SickLeaveHours { get; private set; }

        public bool IsActive { get; private set; }

        public virtual IReadOnlyCollection<DepartmentHistory> DepartmentHistories => _deptHistories.AsReadOnly();

        public Result<DepartmentHistory> AddDepartmentHistory
        (
            int id,
            int shiftId,
            DateOnly startDate,
            DateTime? endDate
        )
        {
            try
            {
                DepartmentHistory? search = _deptHistories.Find(hist => hist.Id == id && hist.ShiftID == shiftId);

                if (search is null)
                {
                    Result<DepartmentHistory> result = DepartmentHistory.Create(id, shiftId, startDate, endDate);
                    if (result.IsSuccess)
                    {
                        _deptHistories.Add(result.Value);
                        return result.Value;
                    }
                    else
                    {
                        return Result<DepartmentHistory>.Failure<DepartmentHistory>(new Error("Employee.AddDepartmentHistory", result.Error.Message));
                    }
                }
                else
                {
                    return Result<DepartmentHistory>.Failure<DepartmentHistory>(new Error("Employee.AddDepartmentHistory", "This is a duplicate department history."));
                }
            }
            catch (Exception ex)
            {
                return Result<DepartmentHistory>.Failure<DepartmentHistory>(new Error("Employee.AddDepartmentHistory", Helpers.GetExceptionMessage(ex)));
            }
        }

        public virtual IReadOnlyCollection<PayHistory> PayHistories => _payHistories.AsReadOnly();

        public Result<PayHistory> AddPayHistory
        (
            int id,
            DateTime rateChangeDate,
            decimal rate,
            PayFrequencyEnum payFrequency
        )
        {
            try
            {
                PayHistory? search = _payHistories.Find(hist => hist.Id == id && hist.RateChangeDate == rateChangeDate);

                if (search is null)
                {
                    Result<PayHistory> result = PayHistory.Create(id, rateChangeDate, rate, payFrequency);
                    if (result.IsSuccess)
                    {
                        _payHistories.Add(result.Value);
                        return result.Value;
                    }
                    else
                    {
                        return Result<PayHistory>.Failure<PayHistory>(new Error("Employee.AddPayHistory", result.Error.Message));
                    }
                }
                else
                {
                    return Result<PayHistory>.Failure<PayHistory>(new Error("Employee.AddPayHistory", "This is a duplicate pay history."));
                }
            }
            catch (Exception ex)
            {
                return Result<PayHistory>.Failure<PayHistory>(new Error("Employee.AddPayHistory", Helpers.GetExceptionMessage(ex)));
            }
        }

        protected override void CheckValidity()
        {
            if (!PersonType.Equals("EM", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Employee must be person type 'EM'.");
        }
    }
}