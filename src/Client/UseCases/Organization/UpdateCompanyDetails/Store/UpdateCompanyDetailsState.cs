using REA.Accounting.Shared.Models.Organization;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public record UpdateCompanyDetailsState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public bool Submitting { get; init; }
        public bool Submitted { get; init; }
        public string? ErrorMessage { get; init; }
        public int CompanyID { get; init; }
        public CompanyCommandModel? CommandModel { get; init; }
        public List<StateCode>? StateCodes { get; init; }
    }
}