using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeeDetails.Store
{
    public record GetEmployeeDetailsSuccessAction(EmployeeDetailReadModel DetailModel);
}