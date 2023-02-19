using REA.Accounting.Application.HumanResources.CreateEmployee;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.BusinessRules.HumanResources
{
    public sealed class CreateEmployeeNameMustBeUnique : BusinessRule<CreateEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repository;

        public CreateEmployeeNameMustBeUnique(IWriteRepositoryManager repo)
            => _repository = repo;

        public override async Task<ValidationResult> Validate(CreateEmployeeCommand employee)
        {
            ValidationResult validationResult = new();

            Result result =
                await _repository.EmployeeAggregate.ValidatePersonNameIsUnique(employee.EmployeeID, employee.FirstName, employee.LastName, employee.MiddleName);

            if (result.IsSuccess)
            {
                validationResult.IsValid = true;

                if (Next is not null)
                    validationResult = await Next.Validate(employee);
            }
            else
            {
                validationResult.Messages.Add(result.Error.Message);
            }

            return validationResult;
        }
    }
}