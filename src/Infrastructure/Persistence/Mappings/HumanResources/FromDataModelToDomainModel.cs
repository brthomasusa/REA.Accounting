using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;

namespace REA.Accounting.Infrastructure.Persistence.Mappings.HumanResources
{
    public static class FromDataModelToDomainModel
    {
        public static Employee MapToEmployeeDomainObject(this PersonDataModel person)
        {
            Employee employee = Employee.Create
            (
                person!.BusinessEntityID,
                person!.PersonType!,
                person!.NameStyle ? NameStyleEnum.Eastern : NameStyleEnum.Western,
                person!.Title!,
                person!.FirstName!,
                person!.LastName!,
                person!.MiddleName!,
                person!.Suffix!,
                person!.Employee!.NationalIDNumber!,
                person!.Employee!.LoginID!,
                person!.Employee!.JobTitle!,
                DateOnly.FromDateTime(person!.Employee!.BirthDate),
                person!.Employee!.MaritalStatus!,
                person!.Employee!.Gender!,
                DateOnly.FromDateTime(person!.Employee!.HireDate),
                person!.Employee!.SalariedFlag,
                person!.Employee!.VacationHours,
                person!.Employee!.SickLeaveHours,
                person!.Employee!.CurrentFlag
            );

            // Add dept histories to employee from person data model
            person!.Employee!.DepartmentHistories.ToList().ForEach(dept =>
                employee.AddDepartmentHistory(dept.BusinessEntityID,
                                              dept.ShiftID,
                                              DateOnly.FromDateTime(dept.StartDate),
                                              dept.EndDate));

            // Add pay histories to employee from person data model
            person!.Employee!.PayHistories.ToList().ForEach(pay =>
                employee.AddPayHistory(
                    pay.BusinessEntityID,
                    pay.RateChangeDate,
                    pay.Rate,
                    (PayFrequencyEnum)pay.PayFrequency
                ));

            return employee;
        }
    }
}