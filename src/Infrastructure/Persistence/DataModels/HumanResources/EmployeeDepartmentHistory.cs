namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class EmployeeDepartmentHistory
    {
        public int BusinessEntityID { get; set; }
        public Int16 DepartmentID { get; set; }
        public byte ShiftID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}