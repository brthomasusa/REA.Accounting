using REA.Accounting.Core.Organization;
using REA.Accounting.Core.Shared;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.IntegrationTests.Repositories
{
    public class CompanyAggregateRepo_Tests : TestBase
    {
        [Fact]
        public async Task GetByIdAsync_CompnayAggregateRepo_ShouldSucceed()
        {
            WriteRepositoryManager writeRepository = new(_dbContext, new NullLogger<WriteRepositoryManager>());

            Result<Company> result = await writeRepository.CompanyAggregate.GetByIdAsync(1);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Departments.Any());
            Assert.True(result.Value.Shifts.Any());
        }

        [Fact]
        public async Task GetByIdAsync_CompnayAggregateRepo_InvalidCompanyID_ShouldFail()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<WriteRepositoryManager>();
            WriteRepositoryManager writeRepository = new(_dbContext, logger);

            Result<Company> result = await writeRepository.CompanyAggregate.GetByIdAsync(11);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Update_CompnayAggregateRepo_ValidData_ShouldSucceed()
        {
            WriteRepositoryManager writeRepository = new(_dbContext, new NullLogger<WriteRepositoryManager>());
            Company company = CompanyTestData.GetCompanyForUpdateWithValidData();

            Result<int> result = await writeRepository.CompanyAggregate.Update(company);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Update_CompnayAggregateRepo_InvalidCompanyID_ShouldFail()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<WriteRepositoryManager>();
            WriteRepositoryManager writeRepository = new(_dbContext, logger);

            Company company = CompanyTestData.GetCompanyForUpdateWithInvalidCompanyID();

            Result<int> result = await writeRepository.CompanyAggregate.Update(company);

            Assert.True(result.IsFailure);
        }
    }
}