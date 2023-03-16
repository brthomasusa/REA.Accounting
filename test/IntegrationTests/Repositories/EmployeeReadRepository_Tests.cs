using REA.Accounting.Core.HumanResources;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;

namespace REA.Accounting.IntegrationTests.Repositories
{
    public class EmployeeReadRepository_Tests : TestBase
    {
        [Fact]
        public async Task GetEmployeeDetailsById_EmployeeReadRepository_ShouldSucceed()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<GetEmployeeDetailByIdResponse> result = await readRepository.EmployeeReadRepository.GetEmployeeDetailsById(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Sánchez", result.Value.LastName);
        }

        [Fact]
        public async Task GetEmployeeDetailsById_EmployeeReadRepository_ShouldFail_WithInvalidEmployeeID()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<ReadRepositoryManager>();
            ReadRepositoryManager readRepository = new(_dapperCtx, logger);

            Result<GetEmployeeDetailByIdResponse> result = await readRepository.EmployeeReadRepository.GetEmployeeDetailsById(111);

            Assert.True(result.IsFailure);
        }
    }
}