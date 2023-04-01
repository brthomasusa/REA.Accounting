using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store
{
    public class DisplayCompanyDetailsFeature : Feature<DisplayCompanyDetailState>
    {
        public override string GetName() => "DisplayCompanyDetail";

        protected override DisplayCompanyDetailState GetInitialState() =>
            new()
            {
                Initialized = false,
                Loading = false,
                ErrorMessage = string.Empty,
                DetailsModel = null,
                CompanyID = 1
            };
    }
}