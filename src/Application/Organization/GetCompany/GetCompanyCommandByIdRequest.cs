using REA.Accounting.Application.Interfaces.Messaging;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;

namespace REA.Accounting.Application.Organization.GetCompany
{
    public sealed record GetCompanyCommandByIdRequest(int CompanyID) : IQuery<GetCompanyCommandByIdResponse>;
}