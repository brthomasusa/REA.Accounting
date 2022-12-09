namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class EmployeePayHistory
    {
        public int BusinessEntityID { get; set; }
        public DateTime RateChangeDate { get; set; }
        public decimal Rate { get; set; }
        public int PayFrequency { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}