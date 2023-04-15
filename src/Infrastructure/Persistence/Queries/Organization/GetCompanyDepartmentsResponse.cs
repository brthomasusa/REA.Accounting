namespace REA.Accounting.Infrastructure.Persistence.Queries.Organization
{
    public sealed class GetCompanyDepartmentsResponse
    {
        public int DepartmentID { get; set; }
        public string? Name { get; set; }
        public string? GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}