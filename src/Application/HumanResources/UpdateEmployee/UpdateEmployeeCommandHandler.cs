using MediatR;
using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.UpdateEmployee
{
    public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, int>
    {
        private const int RETURN_VALUE = 0;
        private readonly IWriteRepositoryManager _repo;

        public UpdateEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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
                            return RETURN_VALUE;
                        }
                        else
                        {
                            return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", updateDbResult.NonSuccessMessage!));
                        }
                    }
                    else
                    {
                        return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", updateDomainObjResult.NonSuccessMessage!));
                    }
                }
                else
                {
                    return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", getResult.NonSuccessMessage!));
                }
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}