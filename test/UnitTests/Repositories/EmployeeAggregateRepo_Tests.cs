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