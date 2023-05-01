using Microsoft.Extensions.Logging;
using REA.Accounting.Infrastructure.Persistence.Interfaces.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources
{
    public sealed class EmployeeReadRepository : IEmployeeReadRepository
    {
        private readonly ILogger<ReadRepositoryManager> _logger;
        private readonly DapperContext _context;
        public EmployeeReadRepository(DapperContext ctx, ILogger<ReadRepositoryManager> logger)
        {
            _logger = logger;
            _context = ctx;
        }

        public async Task<Result<GetEmployeeDetailByIdResponse>> GetEmployeeDetailsById(int employeeId)
            => await GetEmployeeDetailsByIdQuery.Query(employeeId, _context, _logger);

        public async Task<Result<EmployeeDetailReadModel>> GetEmployeeDetailsByIdWithAllInfo(int employeeId)
            => await GetEmployeeDetailsByIdWithAllInfoQuery.Query(employeeId, _context, _logger);

        public async Task<Result<PagedList<EmployeeListItemReadModel>>> GetEmployeeListItemsSearchByLastName(string lastName, PagingParameters pagingParameters)
            => await GetEmployeeListItemsQuery.Query(lastName, pagingParameters, _context, _logger);
    }
}