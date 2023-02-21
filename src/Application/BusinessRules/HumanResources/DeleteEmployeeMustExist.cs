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

            Result result =
                await _repository.EmployeeAggregate.ValidateEmployeeExist(employee.EmployeeID);

            if (result.IsSuccess)
            {
                if (Next is not null)
                {
                    validationResult = await Next.Validate(employee);
                }
            }
            else
            {
                validationResult.Messages.Add(result.Error.Message);
            }

            return validationResult;
        }
    }
}