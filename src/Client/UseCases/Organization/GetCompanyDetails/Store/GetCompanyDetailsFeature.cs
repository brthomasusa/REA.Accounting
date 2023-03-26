using Fluxor;

namespace REA.Accounting.Client.UseCases.Organization.GetCompanyDetails.Store
{
    public class GetCompanyDetailsFeature : Feature<CompanyDetailState>
    {
        public override string GetName() => "GetCompanyDetail";

        protected override CompanyDetailState GetInitialState() =>
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