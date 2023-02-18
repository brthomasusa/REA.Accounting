using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Mappings.HumanResources
{
    public static class FromDomainModelToDataModel
    {
        public static PersonDataModel MapToPersonDataModelForCreate(this Employee employee)
            => new()
            {
                PersonType = employee.PersonType,
                NameStyle = employee.NameStyle != NameStyleEnum.Western,
                Title = employee.Title,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Suffix = employee.Suffix,
                EmailPromotion = (int)employee.EmailPromotions,
                Employee = new EmployeeDataModel()
                {
                    NationalIDNumber = employee.NationalIDNumber,
                    LoginID = employee.LoginID,
                    JobTitle = employee.JobTitle,
                    BirthDate = employee.BirthDate.ToDateTime(new TimeOnly()),
                    MaritalStatus = employee.MaritalStatus,
                    Gender = employee.Gender,
                    HireDate = employee.HireDate.ToDateTime(new TimeOnly()),
                    SalariedFlag = employee.IsSalaried,
                    VacationHours = employee.VacationHours,
                    SickLeaveHours = employee.SickLeaveHours,
                    CurrentFlag = employee.IsActive
                }
            };
    }
}