namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class Company
    {
        public int BusinessEntityID { get; set; }



        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CompanyName { get; set; }
        public string? LegalName { get; set; }
        public string? EIN { get; set; }
        public string? WebsiteUrl { get; set; }
    }
}