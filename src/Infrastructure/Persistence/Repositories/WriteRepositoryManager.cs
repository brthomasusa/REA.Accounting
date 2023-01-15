using REA.Accounting.Core.Interfaces;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Repositories
{
    public class WriteRepositoryManager : IWriteRepositoryManager
    {
        private readonly EfCoreContext _context;
        private readonly Lazy<IEmployeeAggregateRepository> _employeeRepository;

        public WriteRepositoryManager(EfCoreContext context)
        {
            _context = context;
            _employeeRepository = new Lazy<IEmployeeAggregateRepository>(() => new EmployeeAggregateRepository(_context));
        }

        public IEmployeeAggregateRepository EmployeeAggregate => _employeeRepository.Value;
    }
}