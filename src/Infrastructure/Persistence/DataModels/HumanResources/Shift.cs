namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class Shift
    {
        public byte ShiftID { get; set; }
        public string? Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}