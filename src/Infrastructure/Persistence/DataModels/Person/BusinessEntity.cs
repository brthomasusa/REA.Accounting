using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class BusinessEntity
    {
        public int BusinessEntityID { get; set; }
        public virtual Company? Company { get; set; }
        public virtual PersonDataModel? PersonDataModel { get; set; }
        public virtual BusinessEntityAddress? BusinessEntityAddress { get; set; }
        public virtual BusinessEntityContact? BusinessEntityContact { get; set; }
        public virtual HashSet<EmailAddress>? EmailAddresses { get; set; }
        public virtual HashSet<PersonPhone>? Telephones { get; set; }

        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}