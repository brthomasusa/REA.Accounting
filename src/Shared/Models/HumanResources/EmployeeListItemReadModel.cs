namespace REA.Accounting.Shared.Models.HumanResources
{
    public sealed class EmployeeListItemReadModel
    {
        public int BusinessEntityID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public bool Active { get; set; }
        public string? FullName { get; set; }
        public int ManagerID { get; set; }
        public int EmployeesManaged { get; set; }
        public string? ManagerName { get; set; }
    }
}
