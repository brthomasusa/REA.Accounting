namespace REA.Accounting.Shared.Models.HumanResources
{
    public sealed class EmployeeDetailReadModel
    {
        public int BusinessEntityID { get; set; }
        public string? NameStyle { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Suffix { get; set; }
        public string? JobTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhoneNumberType { get; set; }
        public string? EmailAddress { get; set; }
        public string? EmailPromotion { get; set; }
        public string? NationalIDNumber { get; set; }
        public string? LoginID { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? StateProvinceName { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryRegionName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public DateTime HireDate { get; set; }
        public bool Salaried { get; set; }
        public int VacationHours { get; set; }
        public int SickLeaveHours { get; set; }
        public bool Active { get; set; }
    }
}