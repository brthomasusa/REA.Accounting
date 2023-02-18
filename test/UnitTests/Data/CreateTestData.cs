#pragma warning disable CS8603

using Microsoft.EntityFrameworkCore;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

namespace REA.Accounting.UnitTests.Data
{
    public static class CreateTestData
    {
        public static async Task<PersonDataModel> CreateEmployeeAggregateForEditing(EfCoreContext ctx)
        {
            BusinessEntity entity = new()
            {
                BusinessEntityID = 1,
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
                    Employee = new EmployeeDataModel()
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

            await ctx.AddAsync(entity);

            await ctx.SaveChangesAsync();

            return await ctx.Person!.FirstOrDefaultAsync();
        }
    }
}