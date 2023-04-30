using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Queries.HumanResources;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.HumanResources.GetEmployeeDetailsById
{
    public sealed record GetEmployeeListItemsRequest(string LastName, PagingParameters PagingParameters) : IQuery<PagedList<GetEmployeeListItemsResponse>>;
}