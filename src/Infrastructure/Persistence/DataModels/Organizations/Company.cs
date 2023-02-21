using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.Organizations
{
    public class Company
    {
        private List<Department> _departments = new();
        private List<Shift> _shifts = new();

        public Company Create
        (
            List<Department> departments,
            List<Shift> shifts
        )
        {
            _departments = departments;
            _shifts = shifts;

            return new Company();
        }

        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? LegalName { get; set; }
        public string? EIN { get; set; }
        public string? WebsiteUrl { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}