using TestSupport.EfHelpers;
using Microsoft.Extensions.Logging.Abstractions;

using REA.Accounting.Application.HumanResources.GetEmployeeById;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.SharedKernel.Utilities;

using REA.Accounting.UnitTests.TestHelpers;

namespace REA.Accounting.UnitTests.QueryHandlers
{
    public class HrQueryHandler_Tests : IDisposable
    {
        private EfCoreContext? _context;
        private readonly IWriteRepositoryManager _writeRepository;

        public HrQueryHandler_Tests()
        {
            ConfigureDbContextAsync();
            _writeRepository = new WriteRepositoryManager(_context!, new NullLogger<WriteRepositoryManager>());
        }

        public void Dispose()
        {
            _context!.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Handle_GetEmployeeByIdQueryHandler_ShouldSucceed()
        {
            GetEmployeeByIdQuery query = new(EmployeeID: 2);
            GetEmployeeByIdQueryHandler handler = new(_writeRepository);

            Result<GetEmployeeByIdResponse> response = await handler.Handle(query, new CancellationToken());

            Assert.True(response.IsSuccess);
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