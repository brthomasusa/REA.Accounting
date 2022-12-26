using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class BusinessEntityConfig : IEntityTypeConfiguration<BusinessEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessEntity> entity)
        {
            entity.ToTable("BusinessEntity", schema: "Person");
            entity.HasKey(e => e.BusinessEntityID);
            entity.HasOne(p => p.BusinessEntityAddress)
                .WithOne()
                .HasForeignKey<BusinessEntityAddress>(p => p.BusinessEntityID)
                .IsRequired();
            entity.HasOne(p => p.BusinessEntityContact)
                .WithOne()
                .HasForeignKey<BusinessEntityContact>(p => p.BusinessEntityID)
                .IsRequired();
            entity.HasOne(p => p.PersonDataModel)
                .WithOne()
                .HasForeignKey<PersonModel>(p => p.BusinessEntityID)
                .IsRequired();
            entity.HasMany(p => p.EmailAddresses)
                .WithOne()
                .HasForeignKey(p => p.BusinessEntityID)
                .IsRequired();
            entity.HasMany(p => p.Telephones)
                .WithOne()
                .HasForeignKey(p => p.BusinessEntityID)
                .IsRequired();

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedOnAdd();
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