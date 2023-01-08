using TestSupport.EfHelpers;

using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.Repositories
{
    public class EmployeeAggregateRepo_Tests : IDisposable
    {
        private EfCoreContext? _context;

        public EmployeeAggregateRepo_Tests()
        {
            ConfigureDbContextAsync();
        }

        public void Dispose()
        {
            _context!.Dispose();
        }

        [Fact]
        public async Task GetById_EmployeeAggregateRepo_ShouldSucceed()
        {
            IEmployeeAggregateRepository repo = new EmployeeAggregateRepo(_context!);
            OperationResult<Employee> result = await repo.GetByIdAsync(2);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task InsertAsync_EmployeeAggregateRepo_ShouldSucceed()
        {
            Employee employee = GetEmployeeForCreate();
            IEmployeeAggregateRepository repo = new EmployeeAggregateRepo(_context!);

            OperationResult<int> result = await repo.InsertAsync(employee);

            Assert.True(result.Success);
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