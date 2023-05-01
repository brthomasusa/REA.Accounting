using Fluxor;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Store
{
    public class DisplayEmployeeListFeature : Feature<DisplayEmployeeListState>
    {
        public override string GetName() => "DisplayEmployeeList";

        protected override DisplayEmployeeListState GetInitialState() =>
            new()
            {
                Initialized = false,
                Loading = false,
                ErrorMessage = string.Empty,
                EmployeeList = new(),
                MetaData = new() { CurrentPage = 1, PageSize = 15 },
                SearchTerm = string.Empty
            };
    }
}