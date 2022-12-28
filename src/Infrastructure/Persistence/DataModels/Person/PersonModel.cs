namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class PersonModel
    {
        public int BusinessEntityID { get; set; }
        public string? PersonType { get; set; }
        public int NameStyle { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Suffix { get; set; }
        public int EmailPromotion { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual HashSet<EmailAddress> EmailAddresses { get; set; } = new();
        public virtual HashSet<PersonPhone> Telephones { get; set; } = new();
        public virtual HashSet<BusinessEntityAddress> Addresses { get; set; } = new();
    }
}