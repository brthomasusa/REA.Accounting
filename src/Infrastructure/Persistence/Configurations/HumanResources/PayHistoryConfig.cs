using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.HumanResources
{
    internal class PayHistoryConfig : IEntityTypeConfiguration<EmployeePayHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeePayHistory> entity)
        {
            entity.ToTable("EmployeePayHistory", schema: "HumanResources");
            entity.HasKey(e => new { e.BusinessEntityID, e.RateChangeDate });

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.RateChangeDate)
                .HasColumnName("RateChangeDate");
            entity.Property(e => e.Rate)
                .IsRequired()
                .HasColumnName("Rate")
                .HasColumnType("money");
            entity.Property(e => e.PayFrequency)
                .IsRequired()
                .HasColumnName("PayFrequency");
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);
        }
    }
}