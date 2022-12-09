namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class BusinessEntityAddress
    {
        public int BusinessEntityID { get; set; }
        public int AddressID { get; set; }
        public int AddressTypeID { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}