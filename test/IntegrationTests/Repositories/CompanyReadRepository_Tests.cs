using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;

namespace REA.Accounting.IntegrationTests.Repositories
{
    public class CompanyReadRepository_Tests : TestBase
    {
        [Fact]
        public async Task GetCompanyDetailsById_CompanyReadRepository_ShouldSucceed()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<GetCompanyDetailByIdResponse> result = await readRepository.CompanyReadRepository.GetCompanyDetailsById(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Adventure-Works Cycles", result.Value.CompanyName);
        }

        [Fact]
        public async Task GetCompanyDetailsById_CompanyReadRepository_ShouldFail_WithInvalidEmployeeID()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<ReadRepositoryManager>();
            ReadRepositoryManager readRepository = new(_dapperCtx, logger);

            Result<GetCompanyDetailByIdResponse> result = await readRepository.CompanyReadRepository.GetCompanyDetailsById(111);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task GetCompanyCommandById_CompanyReadRepository_ShouldSucceed()
        {
            ReadRepositoryManager readRepository = new(_dapperCtx, new NullLogger<ReadRepositoryManager>());

            Result<GetCompanyCommandByIdResponse> result = await readRepository.CompanyReadRepository.GetCompanyCommandById(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Adventure-Works Cycles", result.Value.CompanyName);
        }

        [Fact]
        public async Task GetCompanyCommandById_CompanyReadRepository_ShouldFail_WithInvalidEmployeeID()
        {
            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            var logger = loggerFactory.CreateLogger<ReadRepositoryManager>();
            ReadRepositoryManager readRepository = new(_dapperCtx, logger);

            Result<GetCompanyCommandByIdResponse> result = await readRepository.CompanyReadRepository.GetCompanyCommandById(111);

            Assert.True(result.IsFailure);
        }
    }
}