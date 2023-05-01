using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Store
{
    public record DisplayEmployeeListState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public string? ErrorMessage { get; init; }
        public List<EmployeeListItemReadModel>? EmployeeList { get; init; }
        public MetaData? MetaData { get; set; }
        public string? SearchTerm { get; set; }
    }
}