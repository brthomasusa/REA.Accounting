using TestSupport.EfHelpers;

using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;

namespace REA.Accounting.UnitTests.Repositories
{
    public class EmployeeAggregateRepo_Tests : IDisposable
    {
        private EfCoreContext? _context;
        private IWriteRepositoryManager _writeRepository;

        public EmployeeAggregateRepo_Tests()
        {
            ConfigureDbContextAsync();
            _writeRepository = new WriteRepositoryManager(_context!);
        }

        public void Dispose()
        {
            _context!.Dispose();
        }

        [Fact]
        public async Task GetById_EmployeeAggregateRepo_ShouldSucceed()
        {
            OperationResult<Employee> result = await _writeRepository.EmployeeAggregate.GetByIdAsync(2);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task InsertAsync_EmployeeAggregateRepo_ShouldSucceed()
        {
            Employee employee = GetEmployeeForCreate();

            OperationResult<int> result = await _writeRepository.EmployeeAggregate.InsertAsync(employee);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_EmployeeAggregateRepo_ShouldSucceed()
        {
            OperationResult<Employee> getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);

            Assert.True(getResult.Success);

            OperationResult<Employee> updateResult =
                getResult.Result.Update("EM", NameStyleEnum.Western, "Mr.", "Jabu", "Jabi", "J", "Sr.",
                                        EmailPromotionEnum.None, "98765432", @"adventure-works\jabi", "Big Dog",
                                        new DateOnly(2000, 1, 31), "M", "M", new DateOnly(2018, 5, 4), true, 5, 1, true);

            Assert.True(updateResult.Success);

            OperationResult<int> saveResult = await _writeRepository.EmployeeAggregate.Update(updateResult.Result);

            Assert.True(saveResult.Success);

            getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);
            Assert.Equal(@"adventure-works\jabi", getResult.Result.LoginID);
        }

        [Fact]
        public async Task Delete_Employee_EmployeeAggregateRepo_ShouldSucceed()
        {
            OperationResult<Employee> getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);

            Assert.True(getResult.Success);

            OperationResult<int> deleteResult = await _writeRepository.EmployeeAggregate.Delete(getResult.Result);

            Assert.True(deleteResult.Success);

            OperationResult<Employee> test = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);
            Assert.Null(test.Result);
        }


        private Employee GetEmployeeForCreate()
            => Employee.Create
                (
                    0,
                    "EM",
                    Core.Shared.NameStyleEnum.Western,
                    "Mr",
                    "John",
                    "Doe",
                    "D",
                    "Senior",
                    "358987145",
                    "adventure-works\\john10",
                    "Tool Designer",
                    new DateOnly(1990, 2, 21),
                    "M",
                    "M",
                    new DateOnly(2023, 1, 5),
                    true,
                    0,
                    0,
                    true
                );

        private async void ConfigureDbContextAsync()
        {
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();
            _context = new EfCoreContext(options);
            _context.Database.EnsureCreated();
            await _context.SeedLookupData();
            await _context.SeedPersonAndHrData();
        }
    }
}