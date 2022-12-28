using REA.Accounting.UnitTests.TestHelpers;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.UnitTests.DbContext.Sqlite
{
    public class PersonHrCreate_Tests
    {
        [Fact]
        public async Task Create_BusinessEntity_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();

            //ATTEMPT
            BusinessEntity businessEntity = new() { RowGuid = Guid.NewGuid(), ModifiedDate = DateTime.Now };
            context.BusinessEntity!.Add(businessEntity);
            await context.SaveChangesAsync();

            BusinessEntity? result = context.BusinessEntity!.FirstOrDefault();

            //VERIFY
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Person_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();

            BusinessEntity entity = new()
            {
                BusinessEntityID = 1,
                PersonModel = new()
                {
                    PersonType = "EM",
                    NameStyle = 0,
                    Title = "Mr.",
                    FirstName = "Johnny",
                    MiddleName = "D",
                    LastName = "Doe",
                    Suffix = "Jr.",
                    EmailPromotion = 2
                }
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            //ATTEMPT
            PersonModel? result = context.Person!.FirstOrDefault();

            //VERIFY
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Employee_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();

            BusinessEntity entity = new()
            {
                BusinessEntityID = 1,
                PersonModel = new()
                {
                    PersonType = "EM",
                    NameStyle = 0,
                    Title = "Mr.",
                    FirstName = "Johnny",
                    MiddleName = "D",
                    LastName = "Doe",
                    Suffix = "Jr.",
                    EmailPromotion = 2
                }
            };

            Employee employee = new()
            {
                BusinessEntityID = entity.BusinessEntityID,
                NationalIDNumber = "245797967",
                LoginID = "adventure-works\terri0",
                JobTitle = "Vice President of Engineering",
                BirthDate = new DateTime(1971, 8, 1),
                MaritalStatus = "M",
                Gender = "M",
                HireDate = new DateTime(2008, 1, 31),
                SalariedFlag = true,
                VacationHours = 1,
                SickLeaveHours = 20,
                CurrentFlag = true
            };

            await context.BusinessEntity!.AddAsync(entity);
            await context.Employee!.AddAsync(employee);
            await context.SaveChangesAsync();

            //ATTEMPT
            Employee? result = context.Employee!.FirstOrDefault();

            //VERIFY
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_AddressForNewEmployee_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();

            // Create BusinessEntity and PersonModel
            BusinessEntity entity = new()
            {
                PersonModel = new()
                {
                    PersonType = "EM",
                    NameStyle = 0,
                    Title = "Mr.",
                    FirstName = "Johnny",
                    MiddleName = "D",
                    LastName = "Doe",
                    Suffix = "Jr.",
                    EmailPromotion = 2
                }
            };

            // Create Address
            Address address = new()
            {
                AddressLine1 = "123 Main Street",
                AddressLine2 = "Suite 123",
                City = "Desoto",
                StateProvinceID = 73,
                PostalCode = "75000"
            };

            await context.BusinessEntity!.AddAsync(entity);
            await context.Address!.AddAsync(address);
            await context.SaveChangesAsync();

            // Create BusinessEntityAddress that links Address to PersonModel
            BusinessEntityAddress businessEntityAddress = new()
            {
                BusinessEntityID = entity!.BusinessEntityID,
                AddressID = address.AddressID,
                AddressTypeID = 2
            };

            // Link the Address to the PersonModel
            entity.PersonModel.Addresses.Add(businessEntityAddress);

            Employee employee = new()
            {
                BusinessEntityID = entity.BusinessEntityID,
                NationalIDNumber = "245797967",
                LoginID = "adventure-works\terri0",
                JobTitle = "Vice President of Engineering",
                BirthDate = new DateTime(1971, 8, 1),
                MaritalStatus = "M",
                Gender = "M",
                HireDate = new DateTime(2008, 1, 31),
                SalariedFlag = true,
                VacationHours = 1,
                SickLeaveHours = 20,
                CurrentFlag = true
            };

            await context.Employee!.AddAsync(employee);
            await context.SaveChangesAsync();

            Employee? result = context.Employee!.FirstOrDefault();
            Assert.NotNull(result);

            //ATTEMPT
            await context.BusinessEntityAddress!.AddAsync(businessEntityAddress);
            var task = Record.ExceptionAsync(async () => await context.SaveChangesAsync());

            //VERIFY
            Assert.Null(task.Exception);
        }

        // [Fact]
        // public async Task Create_AddressForExistingEmployee_ShouldSucceed()
        // {
        //     //SETUP
        //     var options = SqliteInMemory.CreateOptions<EfCoreContext>();

        //     using var context = new EfCoreContext(options);
        //     context.Database.EnsureCreated();
        //     await context.SeedLookupData();
        //     await context.SeedPersonAndHrData();

        //     Employee? employee = await context.Employee!.FindAsync(2);
        //     Assert.NotNull(employee);

        //     Address address = new()
        //     {
        //         AddressID = 0,
        //         AddressLine1 = "123 Main Street",
        //         AddressLine2 = "Suite 123",
        //         City = "Desoto",
        //         StateProvinceID = 73,
        //         PostalCode = "75000"
        //     };

        //     await context.Address!.AddAsync(address);
        //     await context.SaveChangesAsync();

        //     BusinessEntityAddress businessEntityAddress = new()
        //     {
        //         BusinessEntityID = employee!.BusinessEntityID,
        //         AddressID = address.AddressID,
        //         AddressTypeID = 2
        //     };

        //     //ATTEMPT
        //     await context.BusinessEntityAddress!.AddAsync(businessEntityAddress);
        //     var task = Record.ExceptionAsync(async () => await context.SaveChangesAsync());

        //     //VERIFY
        //     Assert.Null(task.Exception);
        // }
    }
}