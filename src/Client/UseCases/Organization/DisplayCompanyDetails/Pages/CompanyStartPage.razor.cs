using Microsoft.AspNetCore.Components;
using Fluxor;
using REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Pages
{
    public partial class CompanyStartPage
    {
        [Inject]
        private IState<CompanyDetailState>? GetCompanyDetailState { get; set; }
        [Inject]
        private IDispatcher? Dispatcher { get; set; }

        private CompanyDetailModel? DetailsModel => GetCompanyDetailState!.Value.DetailsModel;
        private bool Loading => GetCompanyDetailState!.Value.Loading;
        private string selectedTab = "generalInfo";

        protected override void OnInitialized()
        {
            if (!GetCompanyDetailState!.Value.Initialized)
            {
                Dispatcher!.Dispatch(new SetDisplayCompanyDetailsAction(GetCompanyDetailState!.Value.CompanyID));
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