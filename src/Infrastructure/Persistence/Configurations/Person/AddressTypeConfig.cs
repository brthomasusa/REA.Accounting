using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class AddressTypeConfig : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> entity)
        {
            entity.ToTable("AddressType", schema: "Person");
            entity.HasKey(e => e.AddressTypeID);
            entity.HasIndex(p => p.Name).IsUnique();
            // entity.HasMany<BusinessEntityAddress>().WithOne().HasForeignKey(p => p.AddressTypeID).IsRequired();

            entity.Property(e => e.AddressTypeID)
                .HasColumnName("AddressTypeID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired()
                .HasDefaultValue(Guid.NewGuid());
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            entity.HasData
            (
                new AddressType { AddressTypeID = 1, Name = "Billing", RowGuid = new Guid("b84f78b1-4efe-4a0e-8cb7-70e9f112f886"), ModifiedDate = new DateTime(2008, 4, 30) },
                new AddressType { AddressTypeID = 2, Name = "Home", RowGuid = new Guid("41bc2ff6-f0fc-475f-8eb9-cec0805aa0f2"), ModifiedDate = new DateTime(2008, 4, 30) },
                new AddressType { AddressTypeID = 3, Name = "Main Office", RowGuid = new Guid("8eeec28c-07a2-4fb9-ad0a-42d4a0bbc575"), ModifiedDate = new DateTime(2008, 4, 30) },
                new AddressType { AddressTypeID = 4, Name = "Primary", RowGuid = new Guid("24cb3088-4345-47c4-86c5-17b535133d1e"), ModifiedDate = new DateTime(2008, 4, 30) },
                new AddressType { AddressTypeID = 5, Name = "Shipping", RowGuid = new Guid("b29da3f8-19a3-47da-9daa-15c84f4a83a5"), ModifiedDate = new DateTime(2008, 4, 30) },
                new AddressType { AddressTypeID = 6, Name = "Archive", RowGuid = new Guid("a67f238a-5ba2-444b-966c-0467ed9c427f"), ModifiedDate = new DateTime(2008, 4, 30) }
            );
        }
    }
}