namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public sealed class GetEmployeeDetailByIdResponse
    {
        public int EmployeeID { get; set; }
        public string? NameStyle { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Suffix { get; set; }
        public string? EmailPromotion { get; set; }
        public string? NationalIDNumber { get; set; }
        public string? LoginID { get; set; }
        public string? JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public DateTime HireDate { get; set; }
        public bool Salaried { get; set; }
        public int Vacation { get; set; }
        public int SickLeave { get; set; }
        public bool Active { get; set; }
    }
}