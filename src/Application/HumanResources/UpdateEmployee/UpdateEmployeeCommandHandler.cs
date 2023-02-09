using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.UpdateEmployee
{
    public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, OperationResult<int>>
    {
        private IWriteRepositoryManager _repo;

        public UpdateEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<OperationResult<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OperationResult<Employee> getResult = await _repo.EmployeeAggregate.GetByIdAsync(request.EmployeeID);

                if (getResult.Success)
                {
                    OperationResult<Employee> updateDomainObjResult = getResult.Result.Update
                    (
                        request.PersonType,
                        request.NameStyle ? NameStyleEnum.Eastern : NameStyleEnum.Western,
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
                        OperationResult<int> updateDbResult = await _repo.EmployeeAggregate.Update(updateDomainObjResult.Result);

                        if (updateDbResult.Success)
                        {
                            return OperationResult<int>.CreateSuccessResult(0);
                        }
                        else
                        {
                            return OperationResult<int>.CreateFailure(updateDbResult.NonSuccessMessage!);
                        }
                    }
                    else
                    {
                        return OperationResult<int>.CreateFailure(updateDomainObjResult.NonSuccessMessage!);
                    }
                }
                else
                {
                    return OperationResult<int>.CreateFailure(getResult.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<int>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }
}