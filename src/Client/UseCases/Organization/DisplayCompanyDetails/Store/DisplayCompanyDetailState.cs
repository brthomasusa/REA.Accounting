using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store
{
    public record DisplayCompanyDetailState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public string? ErrorMessage { get; init; }
        public int CompanyID { get; init; }
        public CompanyDetailModel? DetailsModel { get; init; }
    }
}