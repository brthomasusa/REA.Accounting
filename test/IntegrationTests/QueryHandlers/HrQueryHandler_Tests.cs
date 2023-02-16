using REA.Accounting.Application.HumanResources.GetEmployeeById;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.IntegrationTests.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.QueryHandlers
{
    public class HrQueryHandler_Tests : TestBase
    {
        private IWriteRepositoryManager _writeRepository;

        public HrQueryHandler_Tests()
            => _writeRepository = new WriteRepositoryManager(_dbContext);

        [Fact]
        public async Task Handle_GetEmployeeByIdQueryHandler_ShouldSucceed()
        {
            GetEmployeeByIdQuery query = new(EmployeeID: 2);
            GetEmployeeByIdQueryHandler handler = new(_writeRepository);

            Result<GetEmployeeByIdResponse> response = await handler.Handle(query, new CancellationToken());

            Assert.True(response.IsSuccess);
        }


    }
}