using CompanyDataModel = REA.Accounting.Infrastructure.Persistence.DataModels.Organizations.Company;
using CompanyDomainModel = REA.Accounting.Core.Organization.Company;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Infrastructure.Persistence.Mappings.Organizations
{
    public static class CompanyDataModelToDomainModel
    {
        public static Result<CompanyDomainModel> MapToCompanyDomainObject(this CompanyDataModel dataModel)
        {
            Result<CompanyDomainModel> result = CompanyDomainModel.Create
            (
                dataModel.CompanyID,
                dataModel.CompanyName!,
                dataModel.LegalName!,
                dataModel.EIN!,
                dataModel.WebsiteUrl!,
                dataModel.MailAddressLine1!,
                dataModel.DeliveryAddressLine2,
                dataModel.MailCity!,
                dataModel.MailStateProvinceID,
                dataModel.MailPostalCode!,
                dataModel.DeliveryAddressLine1!,
                dataModel.DeliveryAddressLine2,
                dataModel.DeliveryCity!,
                dataModel.DeliveryStateProvinceID,
                dataModel.DeliveryPostalCode!,
                dataModel.Telephone!,
                dataModel.Fax!
            );

            if (result.IsFailure)
                return Result<CompanyDomainModel>.Failure<CompanyDomainModel>(new Error("CompanyDataModelToDomainModel.MapToCompanyDomainObject", result.Error.Message));

            return result.Value;
        }
    }
}