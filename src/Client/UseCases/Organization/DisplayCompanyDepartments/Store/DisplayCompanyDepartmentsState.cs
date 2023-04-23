using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public record DisplayCompanyDepartmentsState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public string? ErrorMessage { get; init; }
        public List<DepartmentReadModel>? DepartmentList { get; init; }
        public MetaData? MetaData { get; set; }
    }
}