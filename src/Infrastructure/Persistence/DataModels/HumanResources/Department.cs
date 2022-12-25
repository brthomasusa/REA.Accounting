namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class Department
    {
        public short DepartmentID { get; set; }
        public string? Name { get; set; }
        public string? GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}