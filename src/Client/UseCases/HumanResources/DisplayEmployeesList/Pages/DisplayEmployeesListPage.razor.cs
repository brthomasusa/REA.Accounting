using Fluxor;
using Microsoft.AspNetCore.Components;
using REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Store;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Pages
{
    public partial class DisplayEmployeesListPage
    {
        [Inject] private NavigationManager? NavManager { get; set; }
        [Inject] private IState<DisplayEmployeeListState>? DisplayEmployeeListState { get; set; }
        [Inject] private IDispatcher? Dispatcher { get; set; }
        private string CachedSearchTerm => DisplayEmployeeListState!.Value.SearchTerm!;
        private bool Loading => DisplayEmployeeListState!.Value.Loading;
        private Func<int, int, Task> PagerChangedEventHandler => OnPagerChanged;

        protected override void OnInitialized()
        {
            if (!DisplayEmployeeListState!.Value.Initialized)
            {
                Dispatcher!.Dispatch
                (
                    new GetEmployeeListAction(DisplayEmployeeListState!.Value.MetaData!.CurrentPage,
                                             DisplayEmployeeListState!.Value.MetaData!.PageSize,
                                             DisplayEmployeeListState!.Value.SearchTerm!)
                );
            }

            base.OnInitialized();
        }

        private async Task OnSearchChanged(string searchTerm)
        {
            Dispatcher!.Dispatch
            (
                new GetEmployeeListAction(1, DisplayEmployeeListState!.Value.MetaData!.PageSize, searchTerm)
            );

            await Task.CompletedTask;
        }

        private async Task OnPagerChanged(int pageNumber, int pageSize)
        {
            Dispatcher!.Dispatch
            (
                new GetEmployeeListAction(pageNumber, pageSize, DisplayEmployeeListState!.Value.SearchTerm!)
            );

            await Task.CompletedTask;
        }

        private void OnActionItemClicked(string action, int employeeId)
        {
            NavManager!.NavigateTo
            (
                action switch
                {
                    "View" => $"UseCases/HumanResources/DisplayEmployeeDetails/Pages/DisplayEmployeeDetailsPage/{employeeId}",
                    "Edit" => $"UseCases/HumanResources/UpdateEmployeeDetails/Pages/UpdateEmployeeDetailsPage/{employeeId}",
                    "Delete" => "UseCases/HumanResources/DisplayEmployeesList/Pages/DisplayEmployeesListPage",
                    _ => throw new ArgumentOutOfRangeException(nameof(action), $"Unexpected menu item: {action}"),
                }
            );
        }

        private void GoToCreatePage()
            => NavManager!.NavigateTo("/UseCases/HumanResources/DisplayEmployeesList/Pages/CreateEmployeeDetailsPage");
    }
}