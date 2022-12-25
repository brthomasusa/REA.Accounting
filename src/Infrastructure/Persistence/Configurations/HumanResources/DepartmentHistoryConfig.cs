using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.HumanResources
{
    internal class DepartmentHistoryConfig : IEntityTypeConfiguration<EmployeeDepartmentHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> entity)
        {
            entity.ToTable("EmployeeDepartmentHistory", schema: "HumanResources");
            entity.HasKey(e => new { e.BusinessEntityID, e.StartDate, e.DepartmentID, e.ShiftID });

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.StartDate)
                .HasColumnName("StartDate");
            entity.Property(e => e.DepartmentID)
                .HasColumnName("DepartmentID")
                .ValueGeneratedNever();
            entity.Property(e => e.ShiftID)
                .HasColumnName("ShiftID")
                .ValueGeneratedNever();
            entity.Property(e => e.EndDate)
                .HasColumnName("EndDate");
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);
        }
    }
}