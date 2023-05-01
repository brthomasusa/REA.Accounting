using Fluxor;
using Microsoft.AspNetCore.Components;
using REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeeDetails.Store;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeeDetails.Pages
{
    public partial class DisplayEmployeeDetailsPage
    {
        [Parameter] public int EmployeeId { get; set; }
        [Inject] private IState<DisplayEmployeeDetailsState>? DisplayEmployeeDetailsState { get; set; }
        [Inject] private IDispatcher? Dispatcher { get; set; }
        [Inject] private NavigationManager? NavManager { get; set; }

        private EmployeeDetailReadModel? DetailsModel => DisplayEmployeeDetailsState!.Value.DetailsModel;
        private bool Loading => DisplayEmployeeDetailsState!.Value.Loading;

        protected override void OnInitialized()
        {
            if (!DisplayEmployeeDetailsState!.Value.Initialized)
            {
                Dispatcher!.Dispatch(new GetEmployeeDetailsAction(EmployeeId));
            }

            base.OnInitialized();
        }

        private void GoToUpdateEmployeeDetailsPage()
        {
            NavManager!.NavigateTo($"/UseCases/HumanResources/UpdateEmployeeDetails/Pages/UpdateEmployeeDetailsPage/{EmployeeId}");
        }
    }
}