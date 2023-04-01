using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public record UpdateCompanyDetailsSubmitAction(CompanyCommandModel CommandModel);
}