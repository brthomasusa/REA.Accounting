using REA.Accounting.Application.BusinessRules.HumanResources;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.CreateEmployee
{
    public sealed class CreateEmployeeBusinessRuleValidator : CommandValidator<CreateEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repo;

        public CreateEmployeeBusinessRuleValidator(IWriteRepositoryManager repo)
            => _repo = repo;

        public override async Task<OperationResult<bool>> Validate(CreateEmployeeCommand command)
        {
            CreateEmployeeNameMustBeUnique verifyNameIsUnique = new(_repo);
            CreateEmployeeEmailMustBeUnique verifyEmailIsUnique = new(_repo);
            CreateEmployeeNationalIdNumberMustBeUnique verifyNationalIdIsUnique = new(_repo);

            verifyNameIsUnique.SetNext(verifyEmailIsUnique);
            verifyEmailIsUnique.SetNext(verifyNationalIdIsUnique);

            ValidationResult result = await verifyNameIsUnique.Validate(command);

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