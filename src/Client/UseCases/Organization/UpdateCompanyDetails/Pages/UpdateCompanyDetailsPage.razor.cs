using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.Snackbar;
using Fluxor;
using FluentValidation;

using REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Pages
{
    public partial class UpdateCompanyDetailsPage
    {
        [Inject] private IState<UpdateCompanyDetailsState>? UpdateCompanyDetailsState { get; set; }
        [Inject] private IDispatcher? Dispatcher { get; set; }
        [Inject] private IMessageService? MessageService { get; set; }
        [Inject] private NavigationManager? NavManager { get; set; }

        private const string ReturnUri = "/UseCases/Organization/DisplayCompanyDetails/Pages/CompanyStartPage";
        private CompanyCommandModel? CommandModel => UpdateCompanyDetailsState!.Value.CommandModel;
        private bool Loading => UpdateCompanyDetailsState!.Value.Loading;
        private bool Saving = false;
        private Validations? _validations;
        private Snackbar? _snackbar;
        private string? _snackBarMessage;


        protected override void OnInitialized()
        {
            if (!UpdateCompanyDetailsState!.Value.Initialized)
            {
                Dispatcher!.Dispatch(new GetCompanyDetailsForEditingAction(UpdateCompanyDetailsState!.Value.CompanyID));
            }
            base.OnInitialized();
        }

        private void HandleValidSubmit()
        {
            Dispatcher!.Dispatch(new UpdateCompanyDetailsSubmitAction(UpdateCompanyDetailsState!.Value.CommandModel!));
        }

        private void OnCancel()
        {

        }
    }
}