using REA.Accounting.Application.HumanResources.UpdateEmployee;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.BusinessRules.HumanResources
{
    public sealed class UpdateEmployeeNationalIdNumberMustBeUnique : BusinessRule<UpdateEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repository;

        public UpdateEmployeeNationalIdNumberMustBeUnique(IWriteRepositoryManager repo)
            => _repository = repo;

        public override async Task<ValidationResult> Validate(UpdateEmployeeCommand employee)
        {
            ValidationResult validationResult = new();

            OperationResult<bool> result =
                await _repository.EmployeeAggregate.ValidateNationalIdNumberIsUnique(employee.EmployeeID, employee.NationalID);

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
                    string msg = "Another employee in the database already has this natioanal ID.";
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