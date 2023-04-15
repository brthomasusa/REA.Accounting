using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Application.Organization.GetCompanyDepartments
{
    public sealed record GetCompanyDepartmentsRequest(PagingParameters PagingParameters) : IQuery<PagedList<GetCompanyDepartmentsResponse>>;
}