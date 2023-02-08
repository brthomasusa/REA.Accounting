using REA.Accounting.Application.BusinessRules.HumanResources;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.DeleteEmployee
{
    public sealed class DeleteEmployeeBusinessRuleValidator : CommandValidator<DeleteEmployeeCommand>
    {
        private IWriteRepositoryManager _repo;

        public DeleteEmployeeBusinessRuleValidator(IWriteRepositoryManager repo)
            => _repo = repo;

        public override async Task<OperationResult<bool>> Validate(DeleteEmployeeCommand command)
        {
            DeleteEmployeeMustExist verifyEmployeeExist = new(_repo); ;

            ValidationResult result = await verifyEmployeeExist.Validate(command);

            if (result.IsValid)
            {
                return OperationResult<bool>.CreateSuccessResult(true);
            }
            else
            {
                return OperationResult<bool>.CreateFailure(result.Messages[0]);
            }
        }
    }
}