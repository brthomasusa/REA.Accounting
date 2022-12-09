using Microsoft.EntityFrameworkCore;

namespace REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources
{
    public class Employee
    {
        public int BusinessEntityID { get; set; }
        public string? NationalIDNumber { get; set; }
        public string? LoginID { get; set; }
        public HierarchyId? OrganizationNode { get; set; }
        public string? JobTitle { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public DateOnly HireDate { get; set; }
        public bool SalariedFlag { get; set; }
        public int VacationHours { get; set; }
        public int SickLeaveHours { get; set; }
        public bool CurrentFlag { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}