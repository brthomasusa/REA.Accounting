using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.GetCompanyShifts
{
    public sealed record GetCompanyShiftsRequest(PagingParameters PagingParameters) : IQuery<PagedList<GetCompanyShiftsResponse>>;
}