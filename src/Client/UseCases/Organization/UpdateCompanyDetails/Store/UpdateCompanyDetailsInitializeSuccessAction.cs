using REA.Accounting.Shared.Models.Organization;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public record UpdateCompanyDetailsInitializeSuccessAction(List<StateCode> StateCodes, CompanyCommandModel CommandModel);
}