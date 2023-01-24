using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.CreateEmployee
{
    public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, int>
    {
        private IWriteRepositoryManager _repo;

        public CreateEmployeeCommandHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = Employee.Create
            (
                request.EmployeeID,
                request.PersonType,
                (NameStyleEnum)request.NameStyle,
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

            OperationResult<PayHistory> payResult = employee.AddPayHistory
            (
                employee.Id,
                request.HireDate,
                request.PayRate,
                (PayFrequencyEnum)request.PayFrequency
            );

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

            OperationResult<PersonEmailAddress> emailResult = employee.AddEmailAddress
            (
                employee.Id,
                0,
                request.EmailAddress
            );

            OperationResult<PersonPhone> phoneResult = employee.AddPhoneNumbers
            (
                employee.Id,
                (PhoneNumberTypeEnum)request.PhoneNumberType,
                request.PhoneNumber
            );

            OperationResult<int> result = await _repo.EmployeeAggregate.InsertAsync(employee);

            return result.Result;
        }
    }
}