using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using REA.Accounting.Core.Interfaces;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.DataModels.Person;
using REA.Accounting.Infrastructure.Persistence.Mappings.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Specifications.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Specifications.Person;
using REA.Accounting.SharedKernel.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

using CompanyDataModel = REA.Accounting.Infrastructure.Persistence.DataModels.Organizations.Company;
using CompanyDomainModel = REA.Accounting.Core.Organization.Company;

namespace REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources
{
    public sealed class CompanyAggregateRepository : ICompanyAggregateRepository
    {
        private readonly ILogger<WriteRepositoryManager> _logger;
        private readonly EfCoreContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyAggregateRepository(EfCoreContext ctx, ILogger<WriteRepositoryManager> logger)
        {
            _context = ctx;
            _unitOfWork = new UnitOfWork(_context);
            _logger = logger;
        }

        public async Task<Result<CompanyDomainModel>> GetByIdAsync(int companyId, bool asNoTracking = false)
        {
            try
            {
                CancellationToken cancellationToken = default;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CompanyAggregateRepository.GetByIdAsync - {Helpers.GetExceptionMessage(ex)}");
                return Result<CompanyDomainModel>.Failure<CompanyDomainModel>(new Error("EmployeeAggregateRepository.GetByIdAsync",
                                                                                         Helpers.GetExceptionMessage(ex)));
            }

            throw new NotImplementedException();
        }

        public Task<Result<int>> InsertAsync(CompanyDomainModel entity) => throw new NotImplementedException();
        public Task<Result<int>> Update(CompanyDomainModel entity) => throw new NotImplementedException();
        public Task<Result<int>> Delete(CompanyDomainModel entity) => throw new NotImplementedException();
    }
}