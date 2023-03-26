using Fluxor;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.GetCompanyDetails.Store
{
    public record CompanyDetailState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public string? ErrorMessage { get; init; }
        public int CompanyID { get; init; }
        public CompanyDetailModel? DetailsModel { get; init; }
    }
}