using REA.Accounting.Infrastructure.Persistence.Interfaces.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces.Organization;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces
{
    public interface IReadRepositoryManager
    {
        IEmployeeReadRepository EmployeeReadRepository { get; }
        ICompanyReadRepository CompanyReadRepository { get; }
    }
}