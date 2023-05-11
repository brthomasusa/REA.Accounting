using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Shared.Models.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.Repositories
{
    public class EmployeeReadRepository_Tests : TestBase
    {
        [Fact]
        public async Task GetEmployeeDetailsByIdWithAllInfo_EmployeeReadRepository_ShouldSucceed()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<EmployeeDetailReadModel> result =
                await readRepository.EmployeeReadRepository.GetEmployeeDetailsByIdWithAllInfo(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Sánchez", result.Value.LastName);
        }

        [Fact]
        public async Task GetEmployeeDetailsByIdWithAllInfo_EmployeeReadRepository_ShouldFail()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<EmployeeDetailReadModel> result =
                await readRepository.EmployeeReadRepository.GetEmployeeDetailsByIdWithAllInfo(300);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task GetEmployeeListItemsSearchByLastName_EmployeeReadRepository_ShouldSucceed()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());
            const string lastName = "A";
            PagingParameters pagingParameters = new(1, 10);

            Result<PagedList<EmployeeListItemReadModel>> result =
                await readRepository.EmployeeReadRepository.GetEmployeeListItemsSearchByLastName(lastName, pagingParameters);

            Assert.True(result.IsSuccess);
            int employees = result.Value.Count;
            Assert.Equal(10, employees);
        }
    }
}