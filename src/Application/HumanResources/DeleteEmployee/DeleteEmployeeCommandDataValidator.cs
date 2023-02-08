using FluentValidation;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed class DeleteEmployeeCommandDataValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandDataValidator()
        {
            RuleFor(employee => employee.EmployeeID)
                                        .GreaterThan(0)
                                        .WithMessage("An ID is required in order to locate the employee to be deleted.");
        }
    }
}