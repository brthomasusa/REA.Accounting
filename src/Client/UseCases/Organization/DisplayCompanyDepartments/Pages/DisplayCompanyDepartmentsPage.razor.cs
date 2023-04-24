using Microsoft.AspNetCore.Components;
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

        private async Task OnSearchChanged(string searchTerm)
        {
            // await SearchEmployeesByName
            //     (
            //         searchTerm,
            //         _employeeState!.Value.EmployeeListFilter,
            //         _employeeState!.Value.PageNumber,
            //         _employeeState!.Value.PageSize
            //     );

            await Task.CompletedTask;
        }


    }
}