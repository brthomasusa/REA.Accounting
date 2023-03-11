using REA.Accounting.Application.Interfaces.Messaging;

namespace REA.Accounting.Application.Organization.UpdateCompany
{
    public sealed record UpdateCompanyCommand
    (
            int CompanyID,
            string CompanyName,
            string LegalName,
            string EIN,
            string CompanyWebSite,
            string MailAddressLine1,
            string? MailAddressLine2,
            string MailCity,
            int MailStateProvinceID,
            string MailPostalCode,
            string DeliveryAddressLine1,
            string? DeliveryAddressLine2,
            string DeliveryCity,
            int DeliveryStateProvinceID,
            string DeliveryPostalCode,
            string Telephone,
            string Fax
    ) : ICommand<int>;
}