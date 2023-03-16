using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;

namespace REA.Accounting.Application.Organization.GetCompanyById
{
    public sealed record GetCompanyByIdRequest(int CompanyID) : IQuery<GetCompanyDetailByIdResponse>;
}