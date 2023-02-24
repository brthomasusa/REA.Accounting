using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Core.HumanResources;
using REA.Accounting.Core.Shared;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
    {
        private readonly IWriteRepositoryManager _repo;

        public GetEmployeeByIdQueryHandler(IWriteRepositoryManager repo)
            => _repo = repo;

        public async Task<Result<GetEmployeeByIdResponse>> Handle
        (
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                Result<Employee> result = await _repo.EmployeeAggregate.GetEmployeeOnlyAsync(request.EmployeeID, true);
                if (result.IsSuccess)
                {
                    Employee employee = result.Value;

                    GetEmployeeByIdResponse response = new
                    (
                        employee.Id,
                        employee.PersonType,
                        employee.NameStyle != NameStyleEnum.Western,
                        employee.Title,
                        employee.FirstName,
                        employee.LastName,
                        employee.MiddleName,
                        employee.Suffix,
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
                else
                {
                    return Result<GetEmployeeByIdResponse>.Failure<GetEmployeeByIdResponse>(new Error("GetEmployeeByIdQueryHandler.Handle", result.Error.Message));
                }
            }
            catch (Exception ex)
            {
                return Result<GetEmployeeByIdResponse>.Failure<GetEmployeeByIdResponse>(new Error("GetEmployeeByIdQueryHandler.Handle", Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}