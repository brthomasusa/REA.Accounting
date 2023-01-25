using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, OperationResult<GetEmployeeByIdResponse>>
    {
        private IWriteRepositoryManager _repo;

        public GetEmployeeByIdQueryHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<OperationResult<GetEmployeeByIdResponse>> Handle
        (
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                OperationResult<Employee> result = await _repo.EmployeeAggregate.GetEmployeeOnlyAsync(request.EmployeeID, true);
                if (result.Success)
                {
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

                    return OperationResult<GetEmployeeByIdResponse>.CreateSuccessResult(response);
                }
                else
                {
                    return OperationResult<GetEmployeeByIdResponse>.CreateFailure(result.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<GetEmployeeByIdResponse>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }
        }
    }
}