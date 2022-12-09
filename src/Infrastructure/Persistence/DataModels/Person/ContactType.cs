namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class ContactType
    {
        public int ContactTypeID { get; set; }
        public string? Name { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}