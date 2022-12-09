namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class EmployeeDepartmentHistory
    {
        public int BusinessEntityID { get; set; }
        public int DepartmentID { get; set; }
        public int ShiftID { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}