using Microsoft.Extensions.Logging;
using REA.Accounting.Core.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Repositories
{
    public class WriteRepositoryManager : IWriteRepositoryManager
    {
        private readonly ILogger<WriteRepositoryManager> _logger;
        private readonly EfCoreContext _context;
        private readonly Lazy<IEmployeeAggregateRepository> _employeeRepository;
        private readonly Lazy<ICompanyAggregateRepository> _companyRepository;

        public WriteRepositoryManager
        (
            EfCoreContext context,
            ILogger<WriteRepositoryManager> logger
        )
        {
            _context = context;
            _logger = logger;

            _employeeRepository = new Lazy<IEmployeeAggregateRepository>(()
                => new EmployeeAggregateRepository(_context, _logger));

            _companyRepository = new Lazy<ICompanyAggregateRepository>(()
                => new CompanyAggregateRepository(_context, _logger));
        }

        public IEmployeeAggregateRepository EmployeeAggregate => _employeeRepository.Value;
        public ICompanyAggregateRepository CompanyAggregate => _companyRepository.Value;
    }
}