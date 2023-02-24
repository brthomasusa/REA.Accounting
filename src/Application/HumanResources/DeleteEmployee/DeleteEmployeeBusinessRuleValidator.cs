using REA.Accounting.Application.BusinessRules.HumanResources;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed class DeleteEmployeeBusinessRuleValidator : CommandValidator<DeleteEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repo;

        public DeleteEmployeeBusinessRuleValidator(IWriteRepositoryManager repo)
            => _repo = repo;

        public override async Task<Result> Validate(DeleteEmployeeCommand command)
        {
            DeleteEmployeeMustExist verifyEmployeeExist = new(_repo);

            ValidationResult result = await verifyEmployeeExist.Validate(command);

            if (result.IsValid)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure(new Error("DeleteEmployeeBusinessRuleValidator.Validate", result.Messages[0]));
            }
        }
    }
}