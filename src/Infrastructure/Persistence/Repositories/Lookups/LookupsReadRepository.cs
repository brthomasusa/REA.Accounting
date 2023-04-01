using Microsoft.Extensions.Logging;
using REA.Accounting.Infrastructure.Persistence.Interfaces.Lookups;
using REA.Accounting.Infrastructure.Persistence.Queries.Lookups;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Repositories.Lookups
{
    public class LookupsReadRepository : ILookupsReadRepository
    {
        private readonly ILogger<ReadRepositoryManager> _logger;
        private readonly DapperContext _context;

        public LookupsReadRepository(DapperContext ctx, ILogger<ReadRepositoryManager> logger)
        {
            _logger = logger;
            _context = ctx;
        }

        public async Task<Result<List<StateCode>>> GetStateCodeIdForUSA()
            => await GetStateCodeIdForUSAQuery.Query(_context, _logger);

        public async Task<Result<List<StateCode>>> GetStateCodeIdForAll()
            => await GetStateCodeIdForAllQuery.Query(_context, _logger);
    }
}