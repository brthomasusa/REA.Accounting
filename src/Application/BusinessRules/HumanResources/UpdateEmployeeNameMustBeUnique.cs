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

            OperationResult<bool> result =
                await _repository.EmployeeAggregate.ValidatePersonNameIsUnique(employee.EmployeeID, employee.FirstName, employee.LastName, employee.MiddleName!);

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
                    string msg = $"An employee named {employee.FirstName} {employee.MiddleName!} {employee.LastName} is already in the database.";
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