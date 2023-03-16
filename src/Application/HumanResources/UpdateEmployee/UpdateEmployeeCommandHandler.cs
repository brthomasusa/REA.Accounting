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
                Result<Employee> getEmployee = await _repo.EmployeeAggregateRepository.GetByIdAsync(request.EmployeeID);

                if (getEmployee.IsSuccess)
                {
                    Result<Employee> updateDomainObjResult = getEmployee.Value.Update
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

                    if (updateDomainObjResult.IsSuccess)
                    {
                        // Employee domain obj update succeeded
                        Result<int> updateDbResult = await _repo.EmployeeAggregateRepository.Update(updateDomainObjResult.Value);

                        if (updateDbResult.IsSuccess)
                        {
                            // db update succeeded
                            return RETURN_VALUE;
                        }
                        else
                        {
                            // db update failed
                            return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", updateDbResult.Error.Message));
                        }
                    }
                    else
                    {
                        // Employee domain obj update failed, probably because of validation errors
                        return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", updateDomainObjResult.Error.Message));
                    }
                }
                else
                {
                    // Employee info could not be retrieved from the db
                    return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", getEmployee.Error.Message));
                }
            }
            catch (Exception ex)
            {
                return Result<int>.Failure<int>(new Error("UpdateEmployeeCommandHandler.Handle", Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}