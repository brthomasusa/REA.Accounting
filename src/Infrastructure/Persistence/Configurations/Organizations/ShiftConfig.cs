using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Organizations
{
    internal class ShiftConfig : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> entity)
        {
            entity.ToTable("Shift", schema: "HumanResources");
            entity.HasKey(e => e.ShiftID);
            entity.HasIndex(p => p.Name).IsUnique();

            entity.Property(e => e.ShiftID)  // tinyint
                .HasColumnName("ShiftID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.StartTime)
                .IsRequired()
                .HasColumnName("StartTime")
                .HasColumnType("time(7)");
            entity.Property(e => e.EndTime)
                .IsRequired()
                .HasColumnName("EndTime")
                .HasColumnType("time(7)");
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            entity.HasData
            (
                new Shift { ShiftID = 1, Name = "Day", StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(15, 0, 0), ModifiedDate = new DateTime(2008, 4, 30) },
                new Shift { ShiftID = 2, Name = "Evening", StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(23, 0, 0), ModifiedDate = new DateTime(2008, 4, 30) },
                new Shift { ShiftID = 16, Name = "Night", StartTime = new TimeSpan(23, 0, 0), EndTime = new TimeSpan(7, 0, 0), ModifiedDate = new DateTime(2008, 4, 30) }
            );
        }
    }
}