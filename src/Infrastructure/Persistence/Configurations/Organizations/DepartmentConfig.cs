using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;

namespace REA.Accounting.Infrastructure.Persistence.Configurations.Organizations
{
    internal class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.ToTable("Department", schema: "HumanResources");
            entity.HasKey(e => e.DepartmentID);
            entity.HasIndex(p => p.Name).IsUnique();

            entity.Property(e => e.DepartmentID)
                .HasColumnName("DepartmentID")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.GroupName)
                .IsRequired()
                .HasColumnName("GroupName")
                .HasColumnType("nvarchar(50)");
            entity.Property(e => e.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            entity.HasData
            (
                new Department { DepartmentID = 1, Name = "Engineering", GroupName = "Research and Development", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 2, Name = "Tool Design", GroupName = "Research and Development", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 3, Name = "Sales", GroupName = "Sales and Marketing", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 4, Name = "Marketing", GroupName = "Sales and Marketing", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 5, Name = "Purchasing", GroupName = "Inventory Management", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 6, Name = "Research and Development", GroupName = "Research and Development", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 7, Name = "Production", GroupName = "Manufacturing", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 8, Name = "Production Control", GroupName = "Manufacturing", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 9, Name = "Human Resources", GroupName = "Executive General and Administration", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 10, Name = "Finance", GroupName = "Executive General and Administration", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 11, Name = "Information Services", GroupName = "Executive General and Administration", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 12, Name = "Document Control", GroupName = "Quality Assurance", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 13, Name = "Quality Assurance", GroupName = "Quality Assurance", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 14, Name = "Facilities and Maintenance", GroupName = "Executive General and Administration", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 15, Name = "Shipping and Receiving", GroupName = "Inventory Management", ModifiedDate = new DateTime(2008, 4, 30) },
                new Department { DepartmentID = 16, Name = "Executive", GroupName = "Executive General and Administration", ModifiedDate = new DateTime(2008, 4, 30) }
            );
        }
    }
}