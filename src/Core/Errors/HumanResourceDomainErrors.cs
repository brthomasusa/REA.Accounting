using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Errors;

public static class HumanResources
{
    public static class Employee
    {
        public static readonly Error DuplicateNatioanlIdNumber = new(
            "Employee.NationalIdNumberMustBeUnique",
            "An employee in the database already has this nationalID.");

        public static readonly Error DuplicateEmployeeName = new(
            "Employee.PersonNameMustBeUnique",
            "There is already an employee in the database with this name.");

        public static readonly Error DuplicateEmailAddress = new(
            "Employee.EmailAddressMustBeUnique",
            "There is already an employee in the database with this email address.");
    }
}