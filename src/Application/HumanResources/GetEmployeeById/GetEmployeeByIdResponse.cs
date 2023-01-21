namespace REA.Accounting.Application.HumanResources.GetEmployeeById
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
            DateTime BirthDate,
            string MaritalStatus,
            string Gender,
            DateTime HireDate,
            bool Salaried,
            int Vacation,
            int SickLeave,
            bool Active
    );
}