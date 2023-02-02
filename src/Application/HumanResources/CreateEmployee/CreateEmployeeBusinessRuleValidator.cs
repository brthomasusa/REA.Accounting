using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.CreateEmployee
{
    public class CreateEmployeeBusinessRuleValidator : CommandValidator<CreateEmployeeCommand>
    {
        private IWriteRepositoryManager _repo;

        public CreateEmployeeBusinessRuleValidator(IWriteRepositoryManager repo)
            => _repo = repo;

        public override async Task<OperationResult<bool>> Validate(CreateEmployeeCommand command)
        {
            return OperationResult<bool>.CreateSuccessResult(true);
        }
    }
}