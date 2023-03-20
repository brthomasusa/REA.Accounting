using Grpc.Core;
using gRPC.Contracts;
using MediatR;
using REA.Accounting.Shared.Models.Organization;
using REA.Accounting.Application.Organization.GetCompany;
using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Server.Contracts
{
    public sealed class CompanyContractService : CompanyContract.CompanyContractBase
    {
        private readonly ISender _sender;
        public CompanyContractService(ISender sender) => _sender = sender;

        public override async Task<CompanyDetail> GetCompanyDetailById(ItemRequest request, ServerCallContext context)
        {
            Result<GetCompanyDetailByIdResponse> result = await _sender.Send(new GetCompanyDetailByIdRequest(CompanyID: request.Id));

            CompanyDetail retVal = new()
            {
                Id = result.Value.CompanyID,
                CompanyName = result.Value.CompanyName,
                LegalName = result.Value.LegalName,
                Ein = result.Value.EIN,
                CompanyWebSite = result.Value.WebsiteUrl,
                MailAddressLine1 = result.Value.MailAddressLine1,
                MailAddressLine2 = result.Value.MailAddressLine2,
                MailCity = result.Value.MailCity,
                // MailStateProvinceID = result.Value.M
            };

            return retVal;
        }

        public override async Task<CompanyCommand> GetCompanyCommandById(ItemRequest request, ServerCallContext context)
        {
            Result<GetCompanyDetailByIdResponse> result = await _sender.Send(new GetCompanyDetailByIdRequest(CompanyID: request.Id));

            CompanyCommand retVal = new()
            {
                Id = result.Value.CompanyID,
                CompanyName = result.Value.CompanyName,
                LegalName = result.Value.LegalName,
                Ein = result.Value.EIN,
                CompanyWebSite = result.Value.WebsiteUrl,
                MailAddressLine1 = result.Value.MailAddressLine1,
                MailAddressLine2 = result.Value.MailAddressLine2,
                MailCity = result.Value.MailCity,
                // MailStateProvinceID = result.Value.M
            };

            return retVal;
        }
    }
}
/*
    int32 id = 1;
    string companyName = 2;
    string legalName = 3;
    string ein = 4;
    string companyWebSite = 5;
    string mailAddressLine1 = 6;
    string mailAddressLine = 7;
    string mailCity = 8;
    int32 mailStateProvinceID = 9;
    string mailPostalCode = 10;
    string deliveryAddressLine1 = 11;
    string deliveryAddressLine2 = 12;
    string deliveryCity = 13;
    int32 deliveryStateProvinceID = 14;
    string deliveryPostalCode = 15;
    string telephone = 16;
    string fax = 17;
*/