using REA.Accounting.Core.Interfaces;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces
{
    public interface IWriteRepositoryManager
    {
        IEmployeeAggregateRepository EmployeeAggregate { get; }
    }
}