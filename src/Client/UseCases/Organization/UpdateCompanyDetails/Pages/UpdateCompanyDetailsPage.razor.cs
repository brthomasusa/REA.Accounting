using Microsoft.AspNetCore.Components;
using Blazorise;
using Fluxor;

using REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Pages
{
    public partial class UpdateCompanyDetailsPage
    {
        [Inject] private IState<UpdateCompanyDetailsState>? UpdateCompanyDetailsState { get; set; }
        [Inject] private IDispatcher? Dispatcher { get; set; }
        [Inject] private NavigationManager? NavManager { get; set; }

        private const string ReturnUri = "/UseCases/Organization/DisplayCompanyDetails/Pages/CompanyStartPage";
        private CompanyCommandModel? CommandModel => UpdateCompanyDetailsState!.Value.CommandModel;
        private bool Loading => UpdateCompanyDetailsState!.Value.Loading;
        private Validations? _validations;
        private readonly string SnackBarMessage = "Company details were successfully updated.";


        protected override void OnInitialized()
        {
            if (!UpdateCompanyDetailsState!.Value.StateCodes!.Any())
            {
                Dispatcher!.Dispatch(new LoadStateCodesAction());
            }

            if (!UpdateCompanyDetailsState!.Value.Initialized)
            {
                Dispatcher!.Dispatch(new LoadCompanyDetailsForEditingAction(UpdateCompanyDetailsState!.Value.CompanyID));
            }

            base.OnInitialized();
        }

        private async Task HandleSubmit()
        {
            if (!await _validations!.ValidateAll())
                return;

            Dispatcher!.Dispatch(new UpdateCompanyDetailsSubmitAction(UpdateCompanyDetailsState!.Value.CommandModel!));
        }

        private void OnCancel()
        {
            NavManager!.NavigateTo(ReturnUri);
            Dispatcher!.Dispatch(new SetUpdateInitializeFlagAction(false));
        }

        private static void ValidateStateCode(ValidatorEventArgs e)
        {
            bool isInt = int.TryParse(e.Value.ToString(), out int stateCodeId);
            bool isValid = (isInt && (stateCodeId > 0));

            e.Status = isValid ? ValidationStatus.Success : ValidationStatus.Error;
        }
    }
}