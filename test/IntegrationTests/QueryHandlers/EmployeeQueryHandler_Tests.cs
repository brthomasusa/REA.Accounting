using REA.Accounting.Application.HumanResources.GetEmployeeDetailsById;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.QueryHandlers
{
    public class EmployeeQueryHandler_Tests : TestBase
    {
        private readonly IReadRepositoryManager _repository;

        public EmployeeQueryHandler_Tests()
            => _repository = new ReadRepositoryManager(_dapperCtx, new NullLogger<ReadRepositoryManager>());

        [Fact]
        public async Task Handle_GetEmployeeDetailByIdQueryHandler_ShouldSucceed()
        {
            GetEmployeeDetailByIdRequest request = new(EmployeeID: 2);
            GetEmployeeDetailByIdQueryHandler handler = new(_repository);

            Result<GetEmployeeDetailByIdResponse> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task Handle_GetEmployeeDetailByIdQueryHandler_ShouldFail_WithInvalidID()
        {
            GetEmployeeDetailByIdRequest request = new(EmployeeID: 2000);
            GetEmployeeDetailByIdQueryHandler handler = new(_repository);

            Result<GetEmployeeDetailByIdResponse> response = await handler.Handle(request, new CancellationToken());

            Assert.True(response.IsFailure);
        }
    }
}