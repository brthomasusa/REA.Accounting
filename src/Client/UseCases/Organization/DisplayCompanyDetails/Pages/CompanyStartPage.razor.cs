using Microsoft.AspNetCore.Components;
using Fluxor;
using REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Pages
{
    public partial class CompanyStartPage
    {
        [Inject]
        private IState<DisplayCompanyDetailState>? DisplayCompanyDetailState { get; set; }
        [Inject]
        private IDispatcher? Dispatcher { get; set; }

        private CompanyDetailModel? DetailsModel => DisplayCompanyDetailState!.Value.DetailsModel;
        private bool Loading => DisplayCompanyDetailState!.Value.Loading;
        private string selectedTab = "generalInfo";

        protected override void OnInitialized()
        {
            if (!DisplayCompanyDetailState!.Value.Initialized)
            {
                Dispatcher!.Dispatch(new SetDisplayCompanyDetailsAction(DisplayCompanyDetailState!.Value.CompanyID));
            }
            base.OnInitialized();
        }

        private Task OnSelectedTabChanged(string name)
        {
            selectedTab = name;

            return Task.CompletedTask;
        }
    }
}