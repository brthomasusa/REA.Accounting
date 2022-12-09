namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class PhoneNumberType
    {
        public int PhoneNumberTypeID { get; set; }
        public string? Name { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}