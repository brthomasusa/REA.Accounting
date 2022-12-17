using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Person
{
    public class BusinessEntity
    {
        public int BusinessEntityID { get; set; }
        public virtual Company? Company { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}