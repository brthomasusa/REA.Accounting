using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.DapperQueries.Organization
{
    public class CompanyQuery_Tests : TestBase
    {
        [Fact]
        public async Task Query_GetCompanyDetailsByIdQuery_ShouldSucceed()
        {
            Result<GetCompanyDetailByIdResponse> result = await GetCompanyDetailsByIdQuery.Query(1, _dapperCtx, new NullLogger<ReadRepositoryManager>());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Query_Query_GetCompanyDetailsByIdQuery_ShouldFail_WhenPassedInvalidID()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<ReadRepositoryManager>();

            Result<GetCompanyDetailByIdResponse> result = await GetCompanyDetailsByIdQuery.Query(100, _dapperCtx, logger);

            Assert.True(result.IsFailure);
        }
    }
}