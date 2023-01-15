using TestSupport.EfHelpers;

using REA.Accounting.Application.HumanResources.Commands.CreateEmployee;
using REA.Accounting.Application.HumanResources.Queries.GetEmployeeById;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.QueryHandlers
{
    public class HrQueryHandler_Tests : IDisposable
    {
        private EfCoreContext? _context;
        private IWriteRepositoryManager _writeRepository;

        public HrQueryHandler_Tests()
        {
            ConfigureDbContextAsync();
            _writeRepository = new WriteRepositoryManager(_context!);
        }

        public void Dispose()
        {
            _context!.Dispose();
        }

        [Fact]
        public async Task Handle_GetEmployeeByIdQueryHandler_ShouldSucceed()
        {
            GetEmployeeByIdQuery query = new(EmployeeID: 2);
            GetEmployeeByIdQueryHandler handler = new(_writeRepository);

            GetEmployeeByIdResponse response = await handler.Handle(query, new CancellationToken());

            Assert.NotNull(response);
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