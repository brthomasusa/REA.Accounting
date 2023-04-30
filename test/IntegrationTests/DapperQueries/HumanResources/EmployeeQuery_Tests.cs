using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.DapperQueries.HumanResources
{
    public class EmployeeQuery_Tests : TestBase
    {
        [Fact]
        public async Task Query_GetEmployeeDetailsByIdQuery_ShouldSucceed()
        {
            Result<GetEmployeeDetailByIdResponse> result = await GetEmployeeDetailsByIdQuery.Query(1, _dapperCtx, new NullLogger<ReadRepositoryManager>());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Query_GetEmployeeDetailsByIdQuery_ShouldFail_WhenPassedInvalidID()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<ReadRepositoryManager>();
            Result<GetEmployeeDetailByIdResponse> result = await GetEmployeeDetailsByIdQuery.Query(100, _dapperCtx, logger);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Query_GetEmployeeDetailsByIdWithAllInfoQuery_ShouldSucceed()
        {
            Result<GetEmployeeDetailsByIdWithAllInfoResponse> result =
                await GetEmployeeDetailsByIdWithAllInfoQuery.Query(1, _dapperCtx, new NullLogger<ReadRepositoryManager>());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Query_GetEmployeeDetailsByIdWithAllInfoQuery_ShouldFail_WhenPassedInvalidID()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<ReadRepositoryManager>();
            Result<GetEmployeeDetailByIdResponse> result = await GetEmployeeDetailsByIdQuery.Query(3, _dapperCtx, logger);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Query_GetEmployeeListItemsQuery_ShouldSucceed()
        {
            const string lastName = "A";
            PagingParameters pagingParameters = new(1, 10);

            Result<PagedList<GetEmployeeListItemsResponse>> result =
                await GetEmployeeListItemsQuery.Query(lastName, pagingParameters, _dapperCtx, new NullLogger<ReadRepositoryManager>());

            Assert.True(result.IsSuccess);
            int employees = result.Value.Count;
            Assert.Equal(4, employees);
        }

    }
}
