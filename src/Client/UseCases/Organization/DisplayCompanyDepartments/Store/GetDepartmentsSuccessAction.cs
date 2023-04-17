using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public record GetDepartmentsSuccessAction(List<DepartmentReadModel> Departments);
}