namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public string? Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}