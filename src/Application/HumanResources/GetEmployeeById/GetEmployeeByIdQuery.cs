using REA.Accounting.Application.Interfaces.Messaging;

namespace REA.Accounting.Application.HumanResources.GetEmployeeById
{
    public sealed record GetEmployeeByIdQuery(int EmployeeID) : IQuery<GetEmployeeByIdResponse>;
}