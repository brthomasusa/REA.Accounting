using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.CreateEmployee
{
    public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, OperationResult<int>>
    {
        private IWriteRepositoryManager _repo;

        public CreateEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<OperationResult<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = Employee.Create
            (
                request.EmployeeID,
                request.PersonType,
                request.NameStyle ? NameStyleEnum.Eastern : NameStyleEnum.Western,
                request.Title!,
                request.FirstName,
                request.LastName,
                request.MiddleName!,
                request.Suffix!,
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

            OperationResult<DepartmentHistory> deptResult = employee.AddDepartmentHistory
            (
                employee.Id,
                request.ShiftID,
                DateOnly.FromDateTime(request.HireDate),
                null
            );
            if (!deptResult.Success)
                return OperationResult<int>.CreateFailure(deptResult.NonSuccessMessage!);

            OperationResult<PayHistory> payResult = employee.AddPayHistory
            (
                employee.Id,
                request.HireDate,
                request.PayRate,
                (PayFrequencyEnum)request.PayFrequency
            );
            if (!payResult.Success)
                return OperationResult<int>.CreateFailure(payResult.NonSuccessMessage!);

            OperationResult<Address> addrResult = employee.AddAddress
            (
                0,
                employee.Id,
                (AddressTypeEnum)request.AddressType,
                request.AddressLine1,
                request.AddressLine2!,
                request.City,
                request.StateCode,
                request.PersonType
            );
            if (!addrResult.Success)
                return OperationResult<int>.CreateFailure(addrResult.NonSuccessMessage!);

            OperationResult<PersonEmailAddress> emailResult = employee.AddEmailAddress
            (
                employee.Id,
                0,
                request.EmailAddress
            );
            if (!emailResult.Success)
                return OperationResult<int>.CreateFailure(emailResult.NonSuccessMessage!);

            OperationResult<PersonPhone> phoneResult = employee.AddPhoneNumbers
            (
                employee.Id,
                (PhoneNumberTypeEnum)request.PhoneNumberType,
                request.PhoneNumber
            );
            if (!phoneResult.Success)
                return OperationResult<int>.CreateFailure(phoneResult.NonSuccessMessage!);

            OperationResult<int> insertDbResult = await _repo.EmployeeAggregate.InsertAsync(employee);
            if (insertDbResult.Success)
            {
                return OperationResult<int>.CreateSuccessResult(insertDbResult.Result);
            }
            else
            {
                return OperationResult<int>.CreateFailure(insertDbResult.NonSuccessMessage!);
            }
        }
    }
}