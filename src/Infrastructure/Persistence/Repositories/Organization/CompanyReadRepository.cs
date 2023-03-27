using Microsoft.Extensions.Logging;
using REA.Accounting.Infrastructure.Persistence.Interfaces.Organization;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Repositories.Organization
{
    public sealed class CompanyReadRepository : ICompanyReadRepository
    {
        private readonly ILogger<ReadRepositoryManager> _logger;
        private readonly DapperContext _context;

        public CompanyReadRepository(DapperContext ctx, ILogger<ReadRepositoryManager> logger)
        {
            _logger = logger;
            _context = ctx;
        }

        public async Task<Result<GetCompanyDetailByIdResponse>> GetCompanyDetailsById(int companyId)
            => await GetCompanyDetailsByIdQuery.Query(companyId, _context, _logger);

        public async Task<Result<GetCompanyCommandByIdResponse>> GetCompanyCommandById(int companyId)
            => await GetCompanyCommandByIdQuery.Query(companyId, _context, _logger);
    }
}