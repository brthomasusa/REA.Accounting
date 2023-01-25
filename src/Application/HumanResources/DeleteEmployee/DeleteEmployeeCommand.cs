using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed record DeleteEmployeeCommand(int EmployeeID) : ICommand<OperationResult<bool>>;
}