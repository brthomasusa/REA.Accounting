using REA.Accounting.Application.Interfaces.Messaging;

namespace REA.Accounting.Application.Organization.GetCompanyById
{
    public sealed record GetCompanyByIdQuery(int CompanyID) : IQuery<GetCompanyByIdResponse>;
}