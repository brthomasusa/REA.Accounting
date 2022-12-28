using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class PersonModelConfig : IEntityTypeConfiguration<PersonModel>
    {
        public void Configure(EntityTypeBuilder<PersonModel> entity)
        {
            entity.ToTable("Person", schema: "Person");
            entity.HasKey(e => e.BusinessEntityID);
            entity.HasCheckConstraint("CK_Person_PersonType", "([PersonType] IS NULL) OR ([PersonType] IN ('GC','SP','EM','IN','VC','SC'))");
            entity.HasCheckConstraint("CK_Person_EmailPromotion", "([EmailPromotion] >= 0 AND [EmailPromotion] <= 2)");

            entity.HasMany(p => p.EmailAddresses)
                .WithOne()
                .HasForeignKey(p => p.BusinessEntityID);
            entity.HasMany(p => p.Telephones)
                .WithOne()
                .HasForeignKey(p => p.BusinessEntityID);
            entity.HasMany(p => p.Addresses)
                .WithOne()
                .HasForeignKey(p => p.BusinessEntityID);

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.PersonType)
                .IsRequired()
                .HasColumnName("PersonType")
                .HasColumnType("nchar(2)");
            entity.Property(e => e.NameStyle)
                .HasColumnName("NameStype")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue(0);
            entity.Property(e => e.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar(8)");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.MiddleName)
                .HasColumnName("MiddleName")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.Suffix)
                .HasColumnName("Suffix")
                .HasColumnType("nvarchar(8)");
            entity.Property(e => e.EmailPromotion)
                .HasColumnName("EmailPromotion")
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValue(0);
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