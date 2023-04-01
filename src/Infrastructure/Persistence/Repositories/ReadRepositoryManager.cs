using Microsoft.Extensions.Logging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Interfaces.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces.Lookups;
using REA.Accounting.Infrastructure.Persistence.Interfaces.Organization;
using REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Repositories.Lookups;
using REA.Accounting.Infrastructure.Persistence.Repositories.Organization;

namespace REA.Accounting.Infrastructure.Persistence.Repositories
{
    public sealed class ReadRepositoryManager : IReadRepositoryManager
    {
        private readonly ILogger<ReadRepositoryManager> _logger;
        private readonly DapperContext _context;
        private readonly Lazy<IEmployeeReadRepository> _employeeRepository;
        private readonly Lazy<ICompanyReadRepository> _companyRepository;
        private readonly Lazy<ILookupsReadRepository> _lookupsRepository;

        public ReadRepositoryManager(DapperContext ctx, ILogger<ReadRepositoryManager> logger)
        {
            _context = ctx;
            _logger = logger;

            _employeeRepository = new Lazy<IEmployeeReadRepository>(()
                => new EmployeeReadRepository(_context, _logger));

            _companyRepository = new Lazy<ICompanyReadRepository>(()
                => new CompanyReadRepository(_context, _logger));

            _lookupsRepository = new Lazy<ILookupsReadRepository>(()
                => new LookupsReadRepository(_context, _logger));
        }

        public IEmployeeReadRepository EmployeeReadRepository => _employeeRepository.Value;
        public ICompanyReadRepository CompanyReadRepository => _companyRepository.Value;
        public ILookupsReadRepository LookupsReadRepository => _lookupsRepository.Value;
    }
}