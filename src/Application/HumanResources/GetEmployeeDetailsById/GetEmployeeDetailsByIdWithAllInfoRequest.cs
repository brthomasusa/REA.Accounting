using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed record GetEmployeeDetailsByIdWithAllInfoRequest(int EmployeeID) : IQuery<EmployeeDetailReadModel>;
}