using Fluxor;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public class DisplayCompanyDepartmentsFeature : Feature<DisplayCompanyDepartmentsState>
    {
        public override string GetName() => "DisplayCompanyDepartments";

        protected override DisplayCompanyDepartmentsState GetInitialState() =>
            new()
            {
                Initialized = false,
                Loading = false,
                ErrorMessage = string.Empty,
                DepartmentList = new(),
                MetaData = new() { CurrentPage = 1, PageSize = 15 },
                SearchTerm = string.Empty
            };
    }
}