using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.Organization;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.UpdateCompany
{
    public sealed class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand, int>
    {
        private const int RETURN_VALUE = 0;
        private readonly IWriteRepositoryManager _repo;

        public UpdateCompanyCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<int>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Result<Company> getCompany = await _repo.CompanyAggregate.GetByIdAsync(request.CompanyID);

                if (getCompany.IsFailure)
                    return Result<int>.Failure<int>(new Error("UpdateCompanyCommandHandler.Handle", getCompany.Error.Message));

                Result<Company> updateDomainObj = getCompany.Value.Update
                (
                    request.CompanyName,
                    request.LegalName,
                    request.EIN,
                    request.CompanyWebSite,
                    request.MailAddressLine1,
                    request.MailAddressLine2,
                    request.MailCity,
                    request.MailStateProvinceID,
                    request.MailPostalCode,
                    request.DeliveryAddressLine1,
                    request.DeliveryAddressLine2,
                    request.DeliveryCity,
                    request.DeliveryStateProvinceID,
                    request.DeliveryPostalCode,
                    request.Telephone,
                    request.Fax
                );

                if (updateDomainObj.IsFailure)
                    return Result<int>.Failure<int>(new Error("UpdateCompanyCommandHandler.Handle", updateDomainObj.Error.Message));

                Result<int> updateDbResult = await _repo.CompanyAggregate.Update(updateDomainObj.Value);

                if (updateDbResult.IsFailure)
                    return Result<int>.Failure<int>(new Error("UpdateCompanyCommandHandler.Handle", updateDbResult.Error.Message));

                return RETURN_VALUE;
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("UpdateCompanyCommandHandler.Handle", Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}