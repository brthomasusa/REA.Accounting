

namespace REA.Accounting.Infrastructure.Persistence.Queries.HumanResources
{
    public sealed class GetEmployeeListItemsResponse
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
    }
}