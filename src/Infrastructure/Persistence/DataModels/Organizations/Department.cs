namespace REA.Accounting.Infrastructure.Persistence.DataModels.Organizations
{
    public class Department
    {
        public Int16 DepartmentID { get; set; }
        public string? Name { get; set; }
        public string? GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}