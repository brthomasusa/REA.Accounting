using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class ContactTypeConfig : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> entity)
        {
            entity.ToTable("ContactType", schema: "Person");
            entity.HasKey(e => e.ContactTypeID);
            entity.HasIndex(p => p.Name).IsUnique();

            entity.Property(e => e.ContactTypeID)
                .HasColumnName("ContactTypeID")
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
                new ContactType { ContactTypeID = 1, Name = "Accounting Manager", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 2, Name = "Assistant Sales Agent", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 3, Name = "Assistant Sales Representative", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 4, Name = "Coordinator Foreign Markets", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 5, Name = "Export Administrator", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 6, Name = "International Marketing Manager", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 7, Name = "Marketing Assistant", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 8, Name = "Marketing Manager", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 9, Name = "Marketing Representative", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 10, Name = "Order Administrator", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 11, Name = "Owner", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 12, Name = "Owner/Marketing Assistant", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 13, Name = "Product Manager", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 14, Name = "Purchasing Agent", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 15, Name = "Purchasing Manager", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 16, Name = "Regional Account Representative", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 17, Name = "Sales Agent", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 18, Name = "Sales Associate", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 19, Name = "Sales Manager", ModifiedDate = new DateTime(2008, 4, 30) },
                new ContactType { ContactTypeID = 20, Name = "Sales Representative", ModifiedDate = new DateTime(2008, 4, 30) }
            );
        }
    }
}