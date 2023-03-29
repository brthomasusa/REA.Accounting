using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store
{
    public class DisplayCompanyDetailsFeature : Feature<DisplayCompanyDetailState>
    {
        public override string GetName() => "GetCompanyDetail";

        protected override DisplayCompanyDetailState GetInitialState() =>
            new()
            {
                Initialized = false,
                Loading = false,
                ErrorMessage = null,
                DetailsModel = null,
                CompanyID = 1
            };
    }
}