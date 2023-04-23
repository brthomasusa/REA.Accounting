using Microsoft.AspNetCore.Components;
using Blazorise.DataGrid;
using Fluxor;
using REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Pages
{
    public partial class DisplayCompanyDepartmentsPage
    {
        [Inject] private IState<DisplayCompanyDepartmentsState>? DisplayCompanyDepartmentsState { get; set; }
        [Inject] private IDispatcher? Dispatcher { get; set; }
        private List<DepartmentReadModel>? DepartmentList => DisplayCompanyDepartmentsState!.Value.DepartmentList;
        private bool Loading => DisplayCompanyDepartmentsState!.Value.Loading;
        private int TotalCount;
        private int PageSize => DisplayCompanyDepartmentsState!.Value.MetaData!.PageSize;

        protected override void OnInitialized()
        {
            if (!DisplayCompanyDepartmentsState!.Value.Initialized)
            {
                Dispatcher!.Dispatch
                (
                    new GetDepartmentsAction(DisplayCompanyDepartmentsState!.Value.MetaData!.CurrentPage,
                                             DisplayCompanyDepartmentsState!.Value.MetaData!.PageSize)
                );
            }

            base.OnInitialized();
        }

        private void OnReadData(DataGridReadDataEventArgs<DepartmentReadModel> e)
        {
            if (e.ReadDataMode is DataGridReadDataMode.Paging)
            {
                if (!DisplayCompanyDepartmentsState!.Value.Initialized)
                    return;

                Dispatcher!.Dispatch(new GetNextPageOfDataAction(e.Page, e.PageSize));
                TotalCount = DisplayCompanyDepartmentsState!.Value.MetaData!.TotalCount;
                StateHasChanged();
            }
        }

        private void OnSortChanged(DataGridSortChangedEventArgs e)
        {
            Console.WriteLine($"DataGridSortChangedEventArgs: {e.ToJson()}");
        }
    }
}