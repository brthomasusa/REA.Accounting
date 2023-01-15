using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler
    {
        private IEmployeeAggregateRepository _repo;

        public GetEmployeeByIdQueryHandler(IEmployeeAggregateRepository repo)
            => _repo = repo;

        public async Task<GetEmployeeByIdResponse> Handle
        (
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            OperationResult<Employee> result = await _repo.GetEmployeeOnlyAsync(request.employeeID);

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