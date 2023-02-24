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
        private readonly IWriteRepositoryManager _writeRepository;

        public EmployeeAggregateRepo_Tests()
        {
            ConfigureDbContextAsync();
            _writeRepository = new WriteRepositoryManager(_context!);
        }

        public void Dispose()
        {
            _context!.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GetById_EmployeeAggregateRepo_ShouldSucceed()
        {
            Result<Employee> result = await _writeRepository.EmployeeAggregate.GetByIdAsync(2);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task InsertAsync_EmployeeAggregateRepo_ShouldSucceed()
        {
            Employee employee = GetEmployeeForCreate();

            Result<int> result = await _writeRepository.EmployeeAggregate.InsertAsync(employee);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Update_EmployeeAggregateRepo_ShouldSucceed()
        {
            Result<Employee> getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);

            Assert.True(getResult.IsSuccess);

            Result<Employee> updateResult =
                getResult.Value.Update("EM", NameStyleEnum.Western, "Mr.", "Jabu", "Jabi", "J", "Sr.",
                                        EmailPromotionEnum.None, "98765432", @"adventure-works\jabi", "Big Dog",
                                        new DateOnly(2000, 1, 31), "M", "M", new DateOnly(2018, 5, 4), true, 5, 1, true);

            Assert.True(updateResult.IsSuccess);

            Result<int> saveResult = await _writeRepository.EmployeeAggregate.Update(updateResult.Value);

            Assert.True(saveResult.IsSuccess);

            getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);
            Assert.Equal(@"adventure-works\jabi", getResult.Value.LoginID);
        }

        [Fact]
        public async Task Delete_Employee_EmployeeAggregateRepo_ShouldSucceed()
        {
            Result<Employee> getResult = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);

            Assert.True(getResult.IsSuccess);

            Result<int> deleteResult = await _writeRepository.EmployeeAggregate.Delete(getResult.Value);

            Assert.True(deleteResult.IsSuccess);

            Result<Employee> test = await _writeRepository.EmployeeAggregate.GetByIdAsync(16);
            Assert.True(test.IsFailure);
        }

        private static Employee GetEmployeeForCreate()
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