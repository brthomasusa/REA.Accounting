using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class PersonPhoneConfig : IEntityTypeConfiguration<PersonPhone>
    {
        public void Configure(EntityTypeBuilder<PersonPhone> entity)
        {
            entity.ToTable("PersonPhone", schema: "Person");
            entity.HasKey(e => new { e.BusinessEntityID, e.PhoneNumber, e.PhoneNumberTypeID });
            entity.HasIndex(p => p.PhoneNumber).IsUnique();
            entity.HasOne<PhoneNumberType>()
                .WithMany()
                .HasForeignKey(p => p.PhoneNumberTypeID)
                .IsRequired();

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar(25)");
            entity.Property(e => e.PhoneNumberTypeID)
                .HasColumnName("PhoneNumberTypeID");
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);
        }
    }
}