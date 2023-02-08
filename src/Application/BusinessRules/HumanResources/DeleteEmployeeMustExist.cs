using REA.Accounting.Application.HumanResources.DeleteEmployee;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.BusinessRules.HumanResources
{
    public sealed class DeleteEmployeeMustExist : BusinessRule<DeleteEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repository;

        public DeleteEmployeeMustExist(IWriteRepositoryManager repo)
            => _repository = repo;

        public override async Task<ValidationResult> Validate(DeleteEmployeeCommand employee)
        {
            ValidationResult validationResult = new();

            OperationResult<bool> result =
                await _repository.EmployeeAggregate.ValidateEmployeeExist(employee.EmployeeID);

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
                    string msg = $"Delete failed; an employee with ID {employee.EmployeeID} could not be found.";
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