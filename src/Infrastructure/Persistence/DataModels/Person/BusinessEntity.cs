namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class BusinessEntity
    {
        public int BusinessEntityID { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}