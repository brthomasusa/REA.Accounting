using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public record UpdateCompanyDetailsState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public string? ErrorMessage { get; init; }
        public int CompanyID { get; init; }
        public CompanyCommandModel? CommandModel { get; init; }
    }
}