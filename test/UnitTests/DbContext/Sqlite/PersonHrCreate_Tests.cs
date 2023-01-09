#pragma warning disable CS8600

using REA.Accounting.UnitTests.Data;
using REA.Accounting.UnitTests.TestHelpers;
using TestSupport.EfHelpers;

using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.UnitTests.DbContext.Sqlite
{
    public class PersonHrCreate_Tests
    {
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
        public async Task Create_EmployeeAggregate_With_UOW_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();

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
                    EmailPromotion = 2,
                    Employee = new Employee()
                    {
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
                    },
                    EmailAddresses = new List<EmailAddress>()
                    {
                        new EmailAddress
                        {
                            MailAddress = "johnnydoe@adventureworks.com"
                        }
                    },
                    Telephones = new List<PersonPhone>()
                    {
                        new PersonPhone { PhoneNumber = "214-555-4567", PhoneNumberTypeID = 1},
                        new PersonPhone { PhoneNumber = "972-555-1234", PhoneNumberTypeID = 2},
                        new PersonPhone { PhoneNumber = "469-555-4567", PhoneNumberTypeID = 3}
                    }
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
        public async Task Create_AddressForExistingPerson_ShouldSucceed()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();

            using var context = new EfCoreContext(options);
            context.Database.EnsureCreated();
            await context.SeedLookupData();

            // Get PersonModel
            PersonModel person = await CreateTestData.CreateEmployeeAggregateForEditing(context);

            // Create Address
            Address address = new()
            {
                AddressLine1 = "123 Main Street",
                AddressLine2 = "Suite 123",
                City = "Desoto",
                StateProvinceID = 73,
                PostalCode = "75000"
            };

            //ATTEMPT
            await context.Address!.AddAsync(address);

            // Start transaction
            await context.Database.BeginTransactionAsync();

            await context.SaveChangesAsync();  // Causes database to assign AddressID to Address

            // Create BusinessEntityAddress that links Address to PersonModel
            BusinessEntityAddress businessEntityAddress = new()
            {
                BusinessEntityID = person!.BusinessEntityID,
                AddressID = address.AddressID,
                AddressTypeID = 2
            };

            // Link the Address to the PersonModel
            person.Addresses.Add(businessEntityAddress);
            await context.SaveChangesAsync();

            // Commit transaction
            await context.Database.CommitTransactionAsync();

            person = await context.Person!.FindAsync(person.BusinessEntityID);

            //VERIFY
            Assert.Equal(address.AddressLine1, person!.Addresses[0].Address!.AddressLine1);
        }
    }
}