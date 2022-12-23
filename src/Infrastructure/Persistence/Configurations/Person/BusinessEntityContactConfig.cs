using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class BusinessEntityContactConfig : IEntityTypeConfiguration<BusinessEntityContact>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityContact> entity)
        {
            entity.ToTable("BusinessEntityContact", schema: "Person");
            entity.HasKey(e => new { e.BusinessEntityID, e.PersonID, e.ContactTypeID });
            entity.HasOne<PersonDataModel>()
                .WithMany()
                .HasForeignKey(p => p.PersonID)
                .IsRequired();
            entity.HasOne<ContactType>()
                .WithMany()
                .HasForeignKey(p => p.ContactTypeID)
                .IsRequired();

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.PersonID)
                .HasColumnName("PersonID");
            entity.Property(e => e.ContactTypeID)
                .HasColumnName("ContactTypeID");
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