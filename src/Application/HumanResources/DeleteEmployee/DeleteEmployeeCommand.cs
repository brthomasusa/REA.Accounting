using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed record DeleteEmployeeCommand(int EmployeeID) : ICommand<int>;
}