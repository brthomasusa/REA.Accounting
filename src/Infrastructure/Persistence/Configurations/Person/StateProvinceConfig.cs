using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.Sales;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class StateProvinceConfig : IEntityTypeConfiguration<StateProvince>
    {
        public void Configure(EntityTypeBuilder<StateProvince> entity)
        {
            entity.ToTable("StateProvince", schema: "Person");
            entity.HasKey(e => e.StateProvinceID);
            entity.HasIndex(p => p.StateProvinceCode).IsUnique();
            entity.HasIndex(p => p.Name).IsUnique();
            entity.HasOne<CountryRegion>()
                .WithMany()
                .HasForeignKey(p => p.CountryRegionCode)
                .IsRequired();
            entity.HasOne<SalesTerritory>()
                .WithMany()
                .HasForeignKey(p => p.TerritoryID)
                .IsRequired();

            entity.Property(e => e.StateProvinceID)
                .HasColumnName("StateProvinceID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.StateProvinceCode)
                .HasColumnName("StateProvinceCode")
                .HasColumnType("nchar(3)")
                .IsRequired();
            entity.Property(e => e.CountryRegionCode)
                .HasColumnName("CountryRegionCode")
                .HasColumnType("nvarchar(3)")
                .IsRequired();
            entity.Property(e => e.IsOnlyStateProvinceFlag)
                .HasColumnName("IsOnlyStateProvinceFlag")
                .HasColumnType("bit")
                .IsRequired();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.TerritoryID)
                .HasColumnName("TerritoryID")
                .IsRequired();
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