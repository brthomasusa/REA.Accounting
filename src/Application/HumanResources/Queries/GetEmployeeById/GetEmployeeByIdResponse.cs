namespace REA.Accounting.Application.HumanResources.Queries.GetEmployeeById
{
    public sealed record GetEmployeeByIdResponse
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
            bool Active
    );
}