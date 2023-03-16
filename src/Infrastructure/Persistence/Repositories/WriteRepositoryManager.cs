using Microsoft.Extensions.Logging;
using REA.Accounting.Core.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Repositories.Organization;

namespace REA.Accounting.Infrastructure.Persistence.Repositories
{
    public class WriteRepositoryManager : IWriteRepositoryManager
    {
        private readonly ILogger<WriteRepositoryManager> _logger;
        private readonly EfCoreContext _context;
        private readonly Lazy<IEmployeeWriteRepository> _employeeRepository;
        private readonly Lazy<ICompanyWriteRepository> _companyRepository;

        public WriteRepositoryManager
        (
            EfCoreContext context,
            ILogger<WriteRepositoryManager> logger
        )
        {
            _context = context;
            _logger = logger;

            _employeeRepository = new Lazy<IEmployeeWriteRepository>(()
                => new EmployeeWriteRepository(_context, _logger));

            _companyRepository = new Lazy<ICompanyWriteRepository>(()
                => new CompanyWriteRepository(_context, _logger));
        }

        public IEmployeeWriteRepository EmployeeAggregateRepository => _employeeRepository.Value;
        public ICompanyWriteRepository CompanyAggregateRepository => _companyRepository.Value;
    }
}