using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Store
{
    public record GetEmployeeListSuccessAction(List<EmployeeListItemReadModel> Employees, MetaData MetaData, string SearchTerm);
}