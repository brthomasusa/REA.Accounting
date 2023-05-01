using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.SharedKernel.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed record GetEmployeeListItemsRequest(string LastName, PagingParameters PagingParameters) : IQuery<PagedList<EmployeeListItemReadModel>>;
}