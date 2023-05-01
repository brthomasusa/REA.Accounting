using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeeDetails.Store
{
    public record DisplayEmployeeDetailsState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public string? ErrorMessage { get; init; }
        public int EmployeeID { get; init; }
        public EmployeeDetailReadModel? DetailsModel { get; init; }
    }
}