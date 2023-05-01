using Microsoft.AspNetCore.Components;
using Fluxor;
using REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Pages
{
    public partial class DisplayCompanyDepartmentsPage
    {
        [Inject] private NavigationManager? NavManager { get; set; }
        [Inject] private IState<DisplayCompanyDepartmentsState>? DisplayCompanyDepartmentsState { get; set; }
        [Inject] private IDispatcher? Dispatcher { get; set; }
        private string CachedSearchTerm => DisplayCompanyDepartmentsState!.Value.SearchTerm!;
        private bool Loading => DisplayCompanyDepartmentsState!.Value.Loading;
        private Func<int, int, Task> PagerChangedEventHandler => OnPagerChanged;

        protected override void OnInitialized()
        {
            if (!DisplayCompanyDepartmentsState!.Value.Initialized)
            {
                Dispatcher!.Dispatch
                (
                    new GetDepartmentsAction(DisplayCompanyDepartmentsState!.Value.MetaData!.CurrentPage,
                                             DisplayCompanyDepartmentsState!.Value.MetaData!.PageSize,
                                             DisplayCompanyDepartmentsState!.Value.SearchTerm!)
                );
            }

            base.OnInitialized();
        }

        private async Task OnSearchChanged(string searchTerm)
        {
            Dispatcher!.Dispatch
            (
                new GetDepartmentsAction(1, DisplayCompanyDepartmentsState!.Value.MetaData!.PageSize, searchTerm)
            );

            await Task.CompletedTask;
        }

        private async Task OnPagerChanged(int pageNumber, int pageSize)
        {
            Dispatcher!.Dispatch
            (
                new GetDepartmentsAction(pageNumber, pageSize, DisplayCompanyDepartmentsState!.Value.SearchTerm!)
            );

            await Task.CompletedTask;
        }

        private void OnActionItemClicked(string action, int deptId)
        {
            NavManager!.NavigateTo
            (
                action switch
                {
                    "View" => "UseCases/Organization/DisplayCompanyDepartments/Pages/DisplayCompanyDepartmentsPage",
                    "Edit" => "UseCases/Organization/DisplayCompanyDepartments/Pages/DisplayCompanyDepartmentsPage",
                    "Delete" => "UseCases/Organization/DisplayCompanyDepartments/Pages/DisplayCompanyDepartmentsPage",
                    _ => throw new ArgumentOutOfRangeException(nameof(action), $"Unexpected menu item: {action}"),
                }
            );
        }
    }
}