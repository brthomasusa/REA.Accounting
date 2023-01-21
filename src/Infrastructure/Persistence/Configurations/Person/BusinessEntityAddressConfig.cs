using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class BusinessEntityAddressConfig : IEntityTypeConfiguration<BusinessEntityAddress>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityAddress> entity)
        {
            entity.ToTable("BusinessEntityAddress", schema: "Person");
            entity.HasKey(e => new { e.BusinessEntityID, e.AddressID, e.AddressTypeID });
            entity.HasOne<PersonDataModel>()
                .WithMany()
                .HasForeignKey(p => p.BusinessEntityID)
                .IsRequired();
            entity.HasOne(businessEntityAddress => businessEntityAddress.Address)
                .WithMany()
                .HasForeignKey(businessEntityAddress => businessEntityAddress.AddressID)
                .IsRequired();
            entity.HasOne<AddressType>()
                .WithMany()
                .HasForeignKey(p => p.AddressTypeID)
                .IsRequired();

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.AddressID)
                .HasColumnName("AddressID");
            entity.Property(e => e.AddressTypeID)
                .HasColumnName("AddressTypeID");
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