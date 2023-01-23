using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Person
{
    internal class PasswordConfig : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> entity)
        {
            entity.ToTable("Password", schema: "Person");
            entity.HasKey(e => e.BusinessEntityID);

            entity.Property(e => e.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("varchar(128)");
            entity.Property(e => e.PasswordSalt)
                .IsRequired()
                .HasColumnName("PasswordSalt")
                .HasColumnType("varchar(10)");
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