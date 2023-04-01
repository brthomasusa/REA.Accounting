using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Interfaces.Lookups
{
    public interface ILookupsReadRepository
    {
        Task<Result<List<StateCode>>> GetStateCodeIdForUSA();
        Task<Result<List<StateCode>>> GetStateCodeIdForAll();
    }
}