using REA.Accounting.Application.BusinessRules.HumanResources;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.UpdateEmployee
{
    public class UpdateEmployeeBusinessRuleValidator : CommandValidator<UpdateEmployeeCommand>
    {
        private IWriteRepositoryManager _repo;

        public UpdateEmployeeBusinessRuleValidator(IWriteRepositoryManager repo)
            => _repo = repo;

        public override async Task<OperationResult<bool>> Validate(UpdateEmployeeCommand command)
        {
            UpdateEmployeeMustExist verifyEmployeeExist = new(_repo);
            UpdateEmployeeNameMustBeUnique verifyNameIsUnique = new(_repo);
            UpdateEmployeeNationalIdNumberMustBeUnique verifyNationalIdIsUnique = new(_repo);

            verifyEmployeeExist.SetNext(verifyNameIsUnique);
            verifyNameIsUnique.SetNext(verifyNationalIdIsUnique);

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