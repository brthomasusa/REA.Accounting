using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;

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
        public async Task Create_EmployeeAggregate_WithinTransaction_SetOrgNodeWithStoredProc_ShouldSucceed()
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            BusinessEntity entity = EmployeeTestData.GetBusinessEntity();

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            object[] paramItems = new object[]
            {
                    new SqlParameter("@paramEmployeeID", entity.BusinessEntityID),
                    new SqlParameter("@paramManagerID", 285)
            };

            await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC HumanResources.uspUpdateEmployeeOrgNode @paramManagerID, @paramEmployeeID",
                    paramItems
                );

            await transaction.CommitAsync();

            //ATTEMPT
            PersonDataModel? result = await _dbContext.Person!.FindAsync(entity.BusinessEntityID);

            //VERIFY
            Assert.NotNull(result);
        }
    }
}