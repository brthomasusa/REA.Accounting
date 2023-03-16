using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.BusinessRules.HumanResources
{
    public sealed class UpdateEmployeeNameMustBeUnique : BusinessRule<UpdateEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repository;

        public UpdateEmployeeNameMustBeUnique(IWriteRepositoryManager repo)
            => _repository = repo;

        public override async Task<ValidationResult> Validate(UpdateEmployeeCommand employee)
        {
            ValidationResult validationResult = new();

            Result result =
                await _repository.EmployeeAggregateRepository.ValidatePersonNameIsUnique(employee.EmployeeID, employee.FirstName, employee.LastName, employee.MiddleName);

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