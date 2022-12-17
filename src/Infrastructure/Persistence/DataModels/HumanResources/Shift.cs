namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public string? Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}