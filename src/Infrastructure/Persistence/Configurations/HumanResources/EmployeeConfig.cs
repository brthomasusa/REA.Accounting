using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.HumanResources
{
    internal class EmployeeConfig : IEntityTypeConfiguration<EmployeeDataModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeDataModel> entity)
        {
            entity.ToTable("Employee", schema: "HumanResources");
            entity.HasKey(e => e.BusinessEntityID);
            entity.Ignore(e => e.OrganizationNode);
            entity.HasMany(employee => employee.DepartmentHistories)
                  .WithOne()
                  .HasForeignKey(employee => employee.BusinessEntityID)
                  .IsRequired();
            entity.HasMany(employee => employee.PayHistories)
                  .WithOne()
                  .HasForeignKey(employee => employee.BusinessEntityID)
                  .IsRequired();

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.NationalIDNumber)
                .IsRequired()
                .HasColumnName("NationalIDNumber")
                .HasColumnType("nvarchar(15)");
            entity.Property(e => e.LoginID)
                .IsRequired()
                .HasColumnName("LoginID")
                .HasColumnType("nvarchar(256)");
            entity.Property(e => e.JobTitle)
                .IsRequired()
                .HasColumnName("JobTitle")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.BirthDate)
                .IsRequired()
                .HasColumnName("BirthDate")
                .HasColumnType("DATE");
            entity.Property(e => e.MaritalStatus)
                .IsRequired()
                .HasColumnName("MaritalStatus")
                .HasColumnType("nchar(1)");
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasColumnName("Gender")
                .HasColumnType("nchar(1)");
            entity.Property(e => e.HireDate)
                .IsRequired()
                .HasColumnName("HireDate")
                .HasColumnType("DATE");
            entity.Property(e => e.SalariedFlag)
                .IsRequired()
                .HasColumnName("SalariedFlag")
                .HasColumnType("bit")
                .HasDefaultValue(1);
            entity.Property(e => e.VacationHours)
                .IsRequired()
                .HasColumnName("VacationHours")
                .HasColumnType("smallint")
                .HasDefaultValue(0);
            entity.Property(e => e.SickLeaveHours)
                .IsRequired()
                .HasColumnName("SickLeaveHours")
                .HasColumnType("smallint")
                .HasDefaultValue(0);
            entity.Property(e => e.CurrentFlag)
                .IsRequired()
                .HasColumnName("CurrentFlag")
                .HasColumnType("bit")
                .HasDefaultValue(1);
            entity.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired()
                .HasDefaultValue(Guid.NewGuid());
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);
        }
    }
}