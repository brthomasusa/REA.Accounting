using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.CreateEmployee
{
    public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, int>
    {
        private readonly IWriteRepositoryManager _repo;

        public CreateEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = Employee.Create
            (
                request.EmployeeID,
                request.PersonType,
                request.NameStyle ? NameStyleEnum.Eastern : NameStyleEnum.Western,
                request.Title,
                request.FirstName,
                request.LastName,
                request.MiddleName!,
                request.Suffix,
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
                return Result<int>.Failure<int>(new Error("CreateEmployeeCommandHandler.Handle", deptResult.NonSuccessMessage!));

            OperationResult<PayHistory> payResult = employee.AddPayHistory
            (
                employee.Id,
                request.HireDate,
                request.PayRate,
                (PayFrequencyEnum)request.PayFrequency
            );
            if (!payResult.Success)
                return Result<int>.Failure<int>(new Error("CreateEmployeeCommandHandler.Handle", payResult.NonSuccessMessage!));

            OperationResult<Address> addrResult = employee.AddAddress
            (
                0,
                employee.Id,
                (AddressTypeEnum)request.AddressType,
                request.AddressLine1,
                request.AddressLine2,
                request.City,
                request.StateCode,
                request.PersonType
            );
            if (!addrResult.Success)
                return Result<int>.Failure<int>(new Error("CreateEmployeeCommandHandler.Handle", addrResult.NonSuccessMessage!));

            OperationResult<PersonEmailAddress> emailResult = employee.AddEmailAddress
            (
                employee.Id,
                0,
                request.EmailAddress
            );
            if (!emailResult.Success)
                return Result<int>.Failure<int>(new Error("CreateEmployeeCommandHandler.Handle", emailResult.NonSuccessMessage!));

            OperationResult<PersonPhone> phoneResult = employee.AddPhoneNumbers
            (
                employee.Id,
                (PhoneNumberTypeEnum)request.PhoneNumberType,
                request.PhoneNumber
            );
            if (!phoneResult.Success)
                return Result<int>.Failure<int>(new Error("CreateEmployeeCommandHandler.Handle", phoneResult.NonSuccessMessage!));

            Result<int> insertDbResult = await _repo.EmployeeAggregate.InsertAsync(employee);
            if (insertDbResult.IsSuccess)
            {
                return insertDbResult;
            }
            else
            {
                return Result<int>.Failure<int>(new Error("CreateEmployeeCommandHandler.Handle", insertDbResult.Error.Message));
            }
        }
    }
}