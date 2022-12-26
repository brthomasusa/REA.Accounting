using System.Reflection;
using Microsoft.EntityFrameworkCore;

using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.Sales;

namespace REA.Accounting.Infrastructure.Persistence
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        { }

        public DbSet<AddressType>? AddressType { get; set; }
        public DbSet<ContactType>? ContactType { get; set; }
        public DbSet<PhoneNumberType>? PhoneNumberType { get; set; }
        public DbSet<CountryRegion>? CountryRegion { get; set; }
        public DbSet<SalesTerritory>? SalesTerritory { get; set; }
        public DbSet<StateProvince>? StateProvince { get; set; }
        public DbSet<BusinessEntity>? BusinessEntity { get; set; }
        public DbSet<Company>? Company { get; set; }
        public DbSet<BusinessEntityAddress>? BusinessEntityAddress { get; set; }
        public DbSet<BusinessEntityContact>? BusinessEntityContact { get; set; }
        public DbSet<Address>? Address { get; set; }
        public DbSet<PersonModel>? Person { get; set; }
        public DbSet<EmailAddress>? EmailAddress { get; set; }
        public DbSet<PersonPhone>? PersonPhone { get; set; }
        public DbSet<Department>? Department { get; set; }
        public DbSet<Shift>? Shift { get; set; }
        public DbSet<Employee>? Employee { get; set; }
        public DbSet<EmployeeDepartmentHistory>? EmployeeDepartmentHistory { get; set; }
        public DbSet<EmployeePayHistory>? EmployeePayHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}