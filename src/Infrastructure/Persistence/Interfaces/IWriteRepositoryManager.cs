using REA.Accounting.Core.Interfaces;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces
{
    public interface IWriteRepositoryManager
    {
        IEmployeeWriteRepository EmployeeAggregateRepository { get; }
        ICompanyWriteRepository CompanyAggregateRepository { get; }
    }
}