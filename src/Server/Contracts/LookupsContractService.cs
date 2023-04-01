using Empty = Google.Protobuf.WellKnownTypes.Empty;
using Grpc.Core;
using gRPC.Contracts;
using MediatR;
using REA.Accounting.Application.Organization.GetCompany;
using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Server.Contracts
{
    public sealed class LookupsContractService : LookupsContract.LookupsContractBase
    {
        public override Task GetAllStateCodes
        (
            Empty request,
            IServerStreamWriter<StateProvinceCode> responseStream,
            ServerCallContext context
        )
        {

            // return base.GetAllStateCodes(request, responseStream, context);
            throw new NotImplementedException();
        }

        public override Task GetUsStateCodes
        (
            Empty request,
            IServerStreamWriter<StateProvinceCode> responseStream,
            ServerCallContext context
        )
        {
            // return base.GetUsStateCodes(request, responseStream, context);
            throw new NotImplementedException();
        }
    }
}