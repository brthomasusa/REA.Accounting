using Microsoft.AspNetCore.Components;
using Fluxor;

using REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Pages
{
    public partial class UpdateCompanyDetailsPage
    {
        [Inject]
        private IState<UpdateCompanyDetailsState>? UpdateCompanyDetailsState { get; set; }
        [Inject]
        private IDispatcher? Dispatcher { get; set; }
        private CompanyCommandModel? DetailsModel => UpdateCompanyDetailsState!.Value.CommandModel;
        private bool Loading => UpdateCompanyDetailsState!.Value.Loading;
    }
}