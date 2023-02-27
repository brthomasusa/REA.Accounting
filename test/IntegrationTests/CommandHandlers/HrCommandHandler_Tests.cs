using Microsoft.Extensions.Logging.Abstractions;

using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Application.HumanResources.DeleteEmployee;
using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.IntegrationTests.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.IntegrationTests.CommandHandlers
{
    public class HrCommandHandler_Tests : TestBase
    {
        private readonly IWriteRepositoryManager _writeRepository;

        public HrCommandHandler_Tests()
            => _writeRepository = new WriteRepositoryManager(_dbContext, new NullLogger<WriteRepositoryManager>());

        [Fact]
        public async Task Handle_CreateEmployeeCommandHandler_ShouldSucceed()
        {
            CreateEmployeeCommand command = EmployeeTestData.GetValidCreateEmployeeCommand();
            CreateEmployeeCommandHandler handler = new(_writeRepository);

            Result<int> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.True(result.Value > 0);
        }

        [Fact]
        public async Task Handle_UpdateEmployeeCommandHandler_ShouldSucceed()
        {
            UpdateEmployeeCommand command = EmployeeTestData.GetUpdateEmployeeCommand_ValidData();
            UpdateEmployeeCommandHandler handler = new(_writeRepository);

            Result<int> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_DeleteEmployeeCommandHandler_ShouldSucceed()
        {
            DeleteEmployeeCommand command = new(EmployeeID: 273);
            DeleteEmployeeCommandHandler handler = new(_writeRepository);

            Result<int> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
        }
    }
}