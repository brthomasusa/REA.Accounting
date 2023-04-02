using REA.Accounting.Application.Lookups.GetStateCodesForAll;
using REA.Accounting.Application.Lookups.GetStateCodesForUSA;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.QueryHandlers
{
    public class LookupsQueryHandler_Tests : TestBase
    {
        private readonly IReadRepositoryManager _repository;

        public LookupsQueryHandler_Tests()
            => _repository = new ReadRepositoryManager(_dapperCtx, new NullLogger<ReadRepositoryManager>());

        [Fact]
        public async Task Handle_GetStateCodeIdForUSAQueryHandler_ShouldSucceed()
        {
            GetStateCodeIdForUSARequest request = new();
            GetStateCodeIdForUSAQueryHandler handler = new(_repository);

            Result<List<StateCode>> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task Handle_GetStateCodeIdForAllQueryHandler_ShouldSucceed()
        {
            GetStateCodeIdForAllRequest request = new();
            GetStateCodeIdForAllQueryHandler handler = new(_repository);

            Result<List<StateCode>> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }
    }
}