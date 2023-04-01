using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Infrastructure.Persistence.Repositories.Lookups;
using REA.Accounting.Infrastructure.Persistence.Queries.Lookups;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.IntegrationTests.Repositories
{
    public class LookupsReadRepository_Tests : TestBase
    {
        [Fact]
        public async Task GetStateCodeIdForUSA_LookupsReadRepository_ShouldSucceed()
        {
            LookupsReadRepository readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<List<StateCode>> result = await readRepository.GetStateCodeIdForUSA();

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Any());
        }

        [Fact]
        public async Task GetStateCodeIdForUSA_LookupsReadRepositoryManager_ShouldSucceed()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<List<StateCode>> result = await readRepository.LookupsReadRepository.GetStateCodeIdForUSA();

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Any());
        }

        [Fact]
        public async Task GetStateCodeIdForAll_LookupsReadRepository_ShouldSucceed()
        {
            LookupsReadRepository readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<List<StateCode>> result = await readRepository.GetStateCodeIdForAll();

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Any());
        }

        [Fact]
        public async Task GetStateCodeIdForAll_LookupsReadRepositoryManager_ShouldSucceed()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<List<StateCode>> result = await readRepository.LookupsReadRepository.GetStateCodeIdForAll();

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Any());
        }
    }
}