using System.Reflection;
using Microsoft.EntityFrameworkCore;

using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

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
        public DbSet<BusinessEntity>? BusinessEntity { get; set; }
        public DbSet<Company>? Company { get; set; }
        public DbSet<BusinessEntityAddress>? BusinessEntityAddress { get; set; }
        public DbSet<Address>? Address { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}