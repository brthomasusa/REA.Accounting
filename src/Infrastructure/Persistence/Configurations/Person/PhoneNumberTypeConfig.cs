using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class PhoneNumberTypeConfig : IEntityTypeConfiguration<PhoneNumberType>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberType> entity)
        {
            entity.ToTable("PhoneNumberType", schema: "Person");
            entity.HasKey(e => e.PhoneNumberTypeID);
            entity.HasIndex(p => p.Name).IsUnique();

            entity.Property(e => e.PhoneNumberTypeID)
                .HasColumnName("PhoneNumberTypeID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            entity.HasData
            (
                new PhoneNumberType { PhoneNumberTypeID = 1, Name = "Cell", ModifiedDate = new DateTime(2008, 4, 30) },
                new PhoneNumberType { PhoneNumberTypeID = 2, Name = "Home", ModifiedDate = new DateTime(2008, 4, 30) },
                new PhoneNumberType { PhoneNumberTypeID = 3, Name = "Work", ModifiedDate = new DateTime(2008, 4, 30) }
            );
        }
    }
}