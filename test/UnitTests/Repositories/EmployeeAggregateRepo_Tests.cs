using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;
using TestSupport.Helpers;
using Xunit;

using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Specifications;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

using DataModelEmployee = REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources.Employee;
using DomainModelEmployee = REA.Accounting.Core.HumanResources.Employee;

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
            OperationResult<DomainModelEmployee> result = await repo.GetById(2);

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