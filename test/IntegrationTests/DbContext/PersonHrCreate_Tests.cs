using Xunit;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Extensions;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.IntegrationTests.Base;

namespace REA.Accounting.IntegrationTests.DbContext
{
    public class PersonHrCreate_Tests : TestBase
    {
        [Fact]
        public async Task Create_Person_ShouldSucceed()
        {
            //SETUP
            BusinessEntity entity = new()
            {
                BusinessEntityID = 0,
                PersonModel = new()
                {
                    PersonType = "EM",
                    NameStyle = false,
                    Title = "Mr.",
                    FirstName = "Johnny",
                    MiddleName = "D",
                    LastName = "Doe",
                    Suffix = "Jr.",
                    EmailPromotion = 2
                }
            };

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            //ATTEMPT
            PersonDataModel? result = _dbContext.Person!.FirstOrDefault();

            //VERIFY
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_EmployeeAggregate_With_UOW_ShouldSucceed()
        {
            //SETUP

            BusinessEntity entity = new()
            {
                BusinessEntityID = 0,
                PersonModel = new()
                {
                    PersonType = "EM",
                    NameStyle = false,
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

            await _dbContext.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            //ATTEMPT
            PersonDataModel? result = _dbContext.Person!.FirstOrDefault(p => p.FirstName == "Johnny" && p.LastName == "Doe");

            //VERIFY
            Assert.NotNull(result);
        }
    }
}