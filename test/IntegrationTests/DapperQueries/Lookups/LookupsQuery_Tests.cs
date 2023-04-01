using REA.Accounting.Infrastructure.Persistence.Queries.Lookups;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.DapperQueries.Lookups
{
    public class LookupsQuery_Tests : TestBase
    {
        [Fact]
        public async Task Query_GetStateCodeIdForUSAQuery_ShouldSucceed()
        {
            Result<List<StateCode>> result = await GetStateCodeIdForUSAQuery.Query(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Query_GetStateCodeIdForAllQuery_ShouldSucceed()
        {
            Result<List<StateCode>> result = await GetStateCodeIdForAllQuery.Query(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Assert.True(result.IsSuccess);
        }
    }
}