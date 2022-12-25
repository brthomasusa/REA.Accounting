using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.HumanResources
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employee", schema: "HumanResources");
            entity.HasKey(e => e.BusinessEntityID);
            entity.HasOne<PersonDataModel>()
                .WithOne()
                .HasForeignKey<Employee>(p => p.BusinessEntityID)
                .IsRequired();
            entity.Ignore(e => e.OrganizationNode);
            entity.Ignore(e => e.OrganizationLevel);

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
                .HasColumnType("int")
                .HasDefaultValue(0);
            entity.Property(e => e.SickLeaveHours)
                .IsRequired()
                .HasColumnName("SickLeaveHours")
                .HasColumnType("int")
                .HasDefaultValue(0);
            entity.Property(e => e.CurrentFlag)
                .IsRequired()
                .HasColumnName("SalariedFlag")
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