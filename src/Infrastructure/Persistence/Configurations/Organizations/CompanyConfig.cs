using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Organizations
{
    internal class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.ToTable("Company", schema: "Person");
            entity.HasKey(e => e.CompanyID);

            entity.Property(e => e.CompanyID)
                .HasColumnName("CompanyID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasColumnName("CompanyName")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.LegalName)
                .HasColumnName("LegalName")
                .HasColumnType("nvarchar(50)")
                .HasDefaultValue("Same as company name");
            entity.Property(e => e.EIN)
                .IsRequired()
                .HasColumnName("EIN")
                .HasColumnType("nchar(9)");
            entity.Property(e => e.WebsiteUrl)
                .HasColumnName("WebsiteUrl")
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
                new Company()
                {
                    CompanyID = 1,
                    CompanyName = "AdventureWorks Cycles",
                    LegalName = "AdventureWorks Cycles LLC",
                    EIN = "12-3456789",
                    WebsiteUrl = "https:\\www.Adventureworkscycles.com",
                    RowGuid = new Guid("734a8aa4-0686-429c-8192-8bbd214132b7"),
                    ModifiedDate = DateTime.Now
                }
            );
        }
    }
}