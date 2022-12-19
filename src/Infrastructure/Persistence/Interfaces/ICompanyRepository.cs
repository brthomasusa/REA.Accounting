using REA.Accounting.Infrastructure.Persistence.DataModels.Organizations;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.DataModels.HumanResources;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyByIdAsync(int id);
        Task<HashSet<Employee>> GetEmployeesAsync();
        Task<HashSet<BusinessEntity>> GetBusinessEntitiesAsync();
        Task<HashSet<AddressType>> GetAddressTypeAsync();
    }
}