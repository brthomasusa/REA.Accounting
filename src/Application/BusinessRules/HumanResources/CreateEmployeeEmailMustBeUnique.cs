using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.BusinessRules.HumanResources
{
    public sealed class CreateEmployeeEmailMustBeUnique : BusinessRule<CreateEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repository;

        public CreateEmployeeEmailMustBeUnique(IWriteRepositoryManager repo)
            => _repository = repo;

        public override async Task<ValidationResult> Validate(CreateEmployeeCommand employee)
        {
            ValidationResult validationResult = new();

            OperationResult<bool> result =
                await _repository.EmployeeAggregate.ValidateEmployeeEmailIsUnique(employee.EmployeeID, employee.EmailAddress);

            if (result.Success)
            {
                if (result.Result)
                {
                    validationResult.IsValid = true;

                    if (Next is not null)
                    {
                        validationResult = await Next.Validate(employee);
                    }
                }
                else
                {
                    const string msg = "An employee in the database already has this email address.";
                    validationResult.Messages.Add(msg);
                }
            }
            else
            {
                validationResult.Messages.Add(result.NonSuccessMessage!);
            }

            return validationResult;
        }
    }
}