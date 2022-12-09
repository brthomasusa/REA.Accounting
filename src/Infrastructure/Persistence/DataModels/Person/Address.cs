namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class Address
    {
        public int AddressID { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public int StateProvinceID { get; set; }
        public string? PostalCode { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}