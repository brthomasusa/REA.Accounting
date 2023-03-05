using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using REA.Accounting.Core.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Mappings.Organizations;
using REA.Accounting.Infrastructure.Persistence.Specifications.Organizations;
using REA.Accounting.SharedKernel.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

using CompanyDataModel = REA.Accounting.Infrastructure.Persistence.DataModels.Organizations.Company;
using CompanyDomainModel = REA.Accounting.Core.Organization.Company;

namespace REA.Accounting.Infrastructure.Persistence.Repositories.HumanResources
{
    public sealed class CompanyAggregateRepository : ICompanyAggregateRepository
    {
        private const int SUCCESS_MARKER = 0;
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

                var companyDataModel = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        asNoTracking ? _context.Set<CompanyDataModel>().AsNoTracking() : _context.Set<CompanyDataModel>(),
                        new CompanyByIdSpec(companyId)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (companyDataModel is null)
                {
                    string errMsg = $"Unable to retrieve company with ID: {companyId}";
                    _logger.LogWarning($"Code Path: CompanyAggregateRepository.GetByIdAsync - Message: {errMsg}");
                    return Result<CompanyDomainModel>.Failure<CompanyDomainModel>(new Error("CompanyAggregateRepository.GetByIdAsync", errMsg));
                }

                Result<CompanyDomainModel> result = companyDataModel.MapToCompanyDomainObject();
                if (result.IsFailure)
                {
                    _logger.LogWarning($"Code Path: {result.Error.Code} - Message: {result.Error.Message}");
                    return Result<CompanyDomainModel>.Failure<CompanyDomainModel>(new Error("CompanyAggregateRepository.GetByIdAsync", result.Error.Message));
                }

                CompanyDomainModel companyDomainModel = result.Value;

                var departments = await _context.Department!.ToListAsync();
                if (departments.Any())
                {
                    departments.ForEach(dept => companyDomainModel.AddDepartment(dept.DepartmentID, dept.Name!, dept.GroupName!));
                }

                var shifts = await _context.Shift!.ToListAsync();
                if (shifts.Any())
                {
                    shifts.ForEach(shift => companyDomainModel.AddShift(
                        shift.ShiftID, shift.Name!, shift.StartTime.Hours, shift.StartTime.Minutes, shift.EndTime.Hours, shift.EndTime.Minutes
                    ));
                }

                return companyDomainModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CompanyAggregateRepository.GetByIdAsync - {Helpers.GetExceptionMessage(ex)}");
                return Result<CompanyDomainModel>.Failure<CompanyDomainModel>(new Error("EmployeeAggregateRepository.GetByIdAsync",
                                                                                         Helpers.GetExceptionMessage(ex)));
            }
        }

        public Task<Result<int>> InsertAsync(CompanyDomainModel entity) => throw new NotImplementedException();

        public async Task<Result<int>> Update(CompanyDomainModel entity)
        {
            try
            {
                CancellationToken cancellationToken = default;

                var companyDataModel = await
                    SpecificationEvaluator.Default.GetQuery
                    (
                        _context.Set<CompanyDataModel>(),
                        new CompanyByIdSpec(entity.Id)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (companyDataModel is null)
                {
                    string errMsg = $"Update failed. Unable to retrieve company with ID: {entity.Id}";
                    _logger.LogWarning($"Code Path: CompanyAggregateRepository.Update - Message: {errMsg}");
                    return Result<int>.Failure<int>(new Error("CompanyAggregateRepository.Update", errMsg));
                }

                companyDataModel.CompanyName = entity.CompanyName;
                companyDataModel.LegalName = entity.LegalName;
                companyDataModel.EIN = entity.EIN;
                companyDataModel.WebsiteUrl = entity.CompanyWebSite;
                companyDataModel.MailAddressLine1 = entity.MailAddressLine1;
                companyDataModel.MailAddressLine2 = entity.MailAddressLine2;
                companyDataModel.MailCity = entity.MailCity;
                companyDataModel.MailStateProvinceID = entity.MailStateProvinceId;
                companyDataModel.MailPostalCode = entity.MailPostalCode;
                companyDataModel.DeliveryAddressLine1 = entity.DeliveryAddressLine1;
                companyDataModel.DeliveryAddressLine2 = entity.DeliveryAddressLine2;
                companyDataModel.DeliveryCity = entity.DeliveryCity;
                companyDataModel.DeliveryStateProvinceID = entity.DeliveryStateProvinceId;
                companyDataModel.DeliveryPostalCode = entity.DeliveryPostalCode;
                companyDataModel.Telephone = entity.Telephone;
                companyDataModel.Fax = entity.Fax;

                await _unitOfWork.CommitAsync();

                return Result<int>.Success<int>(SUCCESS_MARKER);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CompanyAggregateRepository.Update - {Helpers.GetExceptionMessage(ex)}");
                return Result<int>.Failure<int>(new Error("CompanyAggregateRepository.Update",
                                                           Helpers.GetExceptionMessage(ex)));
            }
        }

        public Task<Result<int>> Delete(CompanyDomainModel entity) => throw new NotImplementedException();
    }
}