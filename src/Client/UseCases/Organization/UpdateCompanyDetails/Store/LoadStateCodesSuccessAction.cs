using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public record LoadStateCodesSuccessAction(List<StateCode>? StateCodes);
}