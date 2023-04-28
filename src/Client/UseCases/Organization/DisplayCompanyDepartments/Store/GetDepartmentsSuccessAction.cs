using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public record GetDepartmentsSuccessAction(List<DepartmentReadModel> Departments, MetaData MetaData, string SearchTerm);
}