using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeById
{
    public sealed record GetEmployeeByIdQuery(int EmployeeID) : IQuery<OperationResult<GetEmployeeByIdResponse>>;
}