using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed record GetEmployeeDetailByIdRequest(int EmployeeID) : IQuery<GetEmployeeDetailByIdResponse>;
}