using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public class DisplayCompanyDepartmentsState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public string? ErrorMessage { get; init; }
        public List<DepartmentReadModel>? DepartmentList { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}