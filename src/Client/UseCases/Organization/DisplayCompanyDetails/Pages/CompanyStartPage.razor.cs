using Microsoft.AspNetCore.Components;
using Fluxor;
using REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Pages
{
    public partial class CompanyStartPage
    {
        [Inject] private IActionSubscriber? ActionSubscriber { get; set; }
        [Inject] private IState<DisplayCompanyDetailState>? DisplayCompanyDetailState { get; set; }
        [Inject] private IDispatcher? Dispatcher { get; set; }
        [Inject] private NavigationManager? NavManager { get; set; }

        private CompanyDetailModel? DetailsModel => DisplayCompanyDetailState!.Value.DetailsModel;
        private bool Loading => DisplayCompanyDetailState!.Value.Loading;
        private string selectedTab = "generalInfo";

        protected override void OnInitialized()
        {
            if (!DisplayCompanyDetailState!.Value.Initialized)
            {
                Dispatcher!.Dispatch(new SetDisplayCompanyDetailsAction(DisplayCompanyDetailState!.Value.CompanyID));
            }

            // ActionSubscriber!.SubscribeToAction<UpdateCompanyDetailsSubmitSuccessAction>(this, ResetUpdateCompanyDetailsStoreToInitialState);
            Console.WriteLine($"DetailModel: {DisplayCompanyDetailState!.Value.DetailsModel}");
            base.OnInitialized();
        }

        private void OnSelectedTabChanged(string name)
        {
            selectedTab = name;
        }

        private void GoToUpdateCompanyDetailsPage()
        {
            NavManager!.NavigateTo("/UseCases/Organization/UpdateCompanyDetails/Pages/UpdateCompanyDetailsPage");
        }

        // private void ResetUpdateCompanyDetailsStoreToInitialState(UpdateCompanyDetailsSubmitSuccessAction _)
        // {
        //     Dispatcher!.Dispatch(new ResetStateToUnInitializedAction());
        // }

        protected override void Dispose(bool disposing)
        {
            ActionSubscriber!.UnsubscribeFromAllActions(this);
            base.Dispose(disposing);
        }
    }
}