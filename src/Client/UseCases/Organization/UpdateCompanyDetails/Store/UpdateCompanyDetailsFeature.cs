using Fluxor;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public class UpdateCompanyDetailsFeature : Feature<UpdateCompanyDetailsState>
    {
        public override string GetName() => "UpdateCompanyDetails";

        protected override UpdateCompanyDetailsState GetInitialState() =>
            new()
            {
                Initialized = false,
                Loading = false,
                Submitting = false,
                Submitted = false,
                ErrorMessage = string.Empty,
                CommandModel = new CompanyCommandModel(),
                CompanyID = 1
            };
    }
}