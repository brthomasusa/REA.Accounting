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
            if (!result.Success)
                Console.WriteLine(result.NonSuccessMessage);

            Employee employee = result.Result;

            GetEmployeeByIdResponse response = new
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
                employee.BirthDate.ToDateTime(new TimeOnly()),
                employee.MaritalStatus,
                employee.Gender,
                employee.HireDate.ToDateTime(new TimeOnly()),
                employee.IsSalaried,
                employee.VacationHours,
                employee.SickLeaveHours,
                employee.IsActive
            );

            return response;
        }
    }
}