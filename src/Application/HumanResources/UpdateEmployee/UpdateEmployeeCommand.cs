using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;

namespace REA.Accounting.Application.HumanResources.UpdateEmployee
{
    public sealed record UpdateEmployeeCommand
    (
            int EmployeeID,
            string PersonType,
            bool NameStyle,
            string? Title,
            string FirstName,
            string LastName,
            string? MiddleName,
            string? Suffix,
            int EmailPromotion,
            string NationalID,
            string Login,
            string JobTitle,
            DateTime BirthDate,
            string MaritalStatus,
            string Gender,
            DateTime HireDate,
            bool Salaried,
            int Vacation,
            int SickLeave,
            bool Active
    ) : ICommand<int>;

}