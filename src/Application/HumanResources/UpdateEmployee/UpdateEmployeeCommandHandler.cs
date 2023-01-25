using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.UpdateEmployee
{
    public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, OperationResult<bool>>
    {
        private IWriteRepositoryManager _repo;

        public UpdateEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<OperationResult<bool>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OperationResult<Employee> getResult = await _repo.EmployeeAggregate.GetByIdAsync(request.EmployeeID);

                if (getResult.Success)
                {
                    OperationResult<Employee> updateDomainObjResult = getResult.Result.Update
                    (
                        request.PersonType,
                        (NameStyleEnum)request.NameStyle,
                        request.Title!,
                        request.FirstName,
                        request.LastName,
                        request.MiddleName!,
                        request.Suffix!,
                        (EmailPromotionEnum)request.EmailPromotion,
                        request.NationalID,
                        request.Login,
                        request.JobTitle,
                        DateOnly.FromDateTime(request.BirthDate),
                        request.MaritalStatus,
                        request.Gender,
                        DateOnly.FromDateTime(request.HireDate),
                        request.Salaried,
                        request.Vacation,
                        request.SickLeave,
                        request.Active
                    );

                    if (updateDomainObjResult.Success)
                    {
                        OperationResult<bool> updateDbResult = await _repo.EmployeeAggregate.Update(updateDomainObjResult.Result);

                        if (updateDbResult.Success)
                        {
                            return OperationResult<bool>.CreateSuccessResult(true);
                        }
                        else
                        {
                            return OperationResult<bool>.CreateFailure(updateDbResult.NonSuccessMessage!);
                        }
                    }
                    else
                    {
                        return OperationResult<bool>.CreateFailure(updateDomainObjResult.NonSuccessMessage!);
                    }
                }
                else
                {
                    return OperationResult<bool>.CreateFailure(getResult.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }
}