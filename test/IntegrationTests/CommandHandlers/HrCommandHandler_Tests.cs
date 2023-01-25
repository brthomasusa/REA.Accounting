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
        private IWriteRepositoryManager _writeRepository;

        public HrCommandHandler_Tests()
            => _writeRepository = new WriteRepositoryManager(_dbContext);

        [Fact]
        public async Task Handle_CreateEmployeeCommandHandler_ShouldSucceed()
        {
            CreateEmployeeCommand command = EmployeeTestData.GetCreateEmployeeCommand();
            CreateEmployeeCommandHandler handler = new(_writeRepository);

            OperationResult<int> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            Assert.True(result.Result > 0);
        }

        [Fact]
        public async Task Handle_UpdateEmployeeCommandHandler_ShouldSucceed()
        {
            UpdateEmployeeCommand command = EmployeeTestData.GetUpdateEmployeeCommand();
            UpdateEmployeeCommandHandler handler = new(_writeRepository);

            OperationResult<bool> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Handle_DeleteEmployeeCommandHandler_ShouldSucceed()
        {
            DeleteEmployeeCommand command = new DeleteEmployeeCommand(EmployeeID: 273);
            DeleteEmployeeCommandHandler handler = new(_writeRepository);

            OperationResult<bool> result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
        }

    }
}