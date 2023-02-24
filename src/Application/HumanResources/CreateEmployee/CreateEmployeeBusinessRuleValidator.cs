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

        public override async Task<Result> Validate(CreateEmployeeCommand command)
        {
            CreateEmployeeNameMustBeUnique verifyNameIsUnique = new(_repo);
            CreateEmployeeEmailMustBeUnique verifyEmailIsUnique = new(_repo);
            CreateEmployeeNationalIdNumberMustBeUnique verifyNationalIdIsUnique = new(_repo);

            verifyNameIsUnique.SetNext(verifyEmailIsUnique);
            verifyEmailIsUnique.SetNext(verifyNationalIdIsUnique);

            ValidationResult result = await verifyNameIsUnique.Validate(command);

            if (result.IsValid)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure(new Error("CreateEmployeeBusinessRuleValidator.Validate", result.Messages[0]));
            }
        }
    }
}