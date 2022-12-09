using REA.Accounting.Core.HumanResources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REA.Accounting.Core.Organization
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyByIdAsync(int id);
        Task<List<Employee>> GetEmployeesAsync();
    }
}