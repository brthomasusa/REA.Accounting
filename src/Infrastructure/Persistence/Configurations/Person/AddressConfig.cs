using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("Address", schema: "Person");
            entity.HasKey(e => e.AddressID);
            entity.HasOne<StateProvince>()
                .WithMany()
                .HasForeignKey(p => p.StateProvinceID)
                .IsRequired();

            entity.Property(e => e.AddressID)
                .HasColumnName("AddressID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.AddressLine1)
                .IsRequired()
                .HasColumnName("AddressLine1")
                .HasColumnType("nvarchar(60)");
            entity.Property(e => e.AddressLine2)
                .HasColumnName("AddressLine2")
                .HasColumnType("nvarchar(60)");
            entity.Property(e => e.City)
                .IsRequired()
                .HasColumnName("City")
                .HasColumnType("nvarchar(30)");
            entity.Property(e => e.StateProvinceID)
                .HasColumnName("StateProvinceID")
                .IsRequired();
            entity.Property(e => e.PostalCode)
                .IsRequired()
                .HasColumnName("PostalCode")
                .HasColumnType("nvarchar(60)");
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