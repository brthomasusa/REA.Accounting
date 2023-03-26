using Microsoft.AspNetCore.Components;
using Fluxor;
using REA.Accounting.Client.UseCases.Organization.GetCompanyDetails.Store;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.GetCompanyDetails.Pages
{
    public partial class CompanyStartPage
    {
        [Inject]
        private IState<CompanyDetailState>? GetCompanyDetailState { get; set; }
        [Inject]
        private IDispatcher? Dispatcher { get; set; }

        private CompanyDetailModel? DetailsModel;
        private bool Loading => GetCompanyDetailState!.Value.Loading
        ;

        protected override void OnInitialized()
        {
            if (!GetCompanyDetailState!.Value.Initialized)
            {
                Dispatcher!.Dispatch(new SetGetCompanyDetailsAction(GetCompanyDetailState!.Value.CompanyID));
                DetailsModel = GetCompanyDetailState!.Value.DetailsModel;
                Dispatcher!.Dispatch(new SetInitializeAction());
            }
            base.OnInitialized();
        }
    }
}