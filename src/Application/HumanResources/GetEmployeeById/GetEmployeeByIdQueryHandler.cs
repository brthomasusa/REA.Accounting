using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
    {
        private IWriteRepositoryManager _repo;

        public GetEmployeeByIdQueryHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<GetEmployeeByIdResponse> Handle
        (
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            OperationResult<Employee> result = await _repo.EmployeeAggregate.GetEmployeeOnlyAsync(request.EmployeeID, true);

            Employee employee = result.Result;

            return new GetEmployeeByIdResponse
            (
                employee.Id,
                employee.PersonType,
                (int)employee.NameStyle,
                employee.Title!,
                employee.FirstName,
                employee.LastName,
                employee.MiddleName!,
                employee.Suffix!,
                employee.NationalIDNumber,
                employee.LoginID,
                employee.JobTitle,
                employee.BirthDate,
                employee.MaritalStatus,
                employee.Gender,
                employee.HireDate,
                employee.IsSalaried,
                employee.VacationHours,
                employee.SickLeaveHours,
                employee.IsActive
            );
        }
    }
}