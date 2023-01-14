using REA.Accounting.Application.Interfaces.Messaging;

namespace REA.Accounting.Application.HumanResources.Commands.CreateEmployee
{
    public sealed record CreateEmployeeCommand
    (
            int EmployeeID,
            string PersonType,
            int NameStyle,
            string? Title,
            string FirstName,
            string LastName,
            string? MiddleName,
            string? Suffix,
            string NationalID,
            string Login,
            string JobTitle,
            DateOnly BirthDate,
            string MaritalStatus,
            string Gender,
            DateOnly HireDate,
            bool Salaried,
            int Vacation,
            int SickLeave,
            bool Active,
            decimal PayRate,
            int PayFrequency,
            int DepartmentID,
            int ShiftID,
            int AddressType,
            string AddressLine1,
            string AddressLine2,
            string City,
            int StateCode,
            string PostalCode,
            string EmailAddress,
            string PhoneNumber,
            int PhoneNumberType
    ) : ICommand<int>;

}