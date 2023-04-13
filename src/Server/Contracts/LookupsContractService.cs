using Empty = Google.Protobuf.WellKnownTypes.Empty;
using Grpc.Core;
using gRPC.Contracts.Lookups;
using MediatR;
using REA.Accounting.Application.Lookups.GetStateCodesForAll;
using REA.Accounting.Application.Lookups.GetStateCodesForUSA;
using REA.Accounting.Shared.Models.Lookups;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Server.Contracts
{
    public sealed class LookupsContractService : LookupsContract.LookupsContractBase
    {
        private readonly ISender _sender;

        public LookupsContractService(ISender sender)
            => _sender = sender;

        public async override Task GetAllStateCodes
        (
            Empty request,
            IServerStreamWriter<StateProvinceCode> responseStream,
            ServerCallContext context
        )
        {
            Result<List<StateCode>> stateCodes = await _sender.Send(new GetStateCodeIdForAllRequest());

            stateCodes.Value.ToList().ForEach(stateCode => responseStream.WriteAsync(
                new StateProvinceCode { Id = stateCode.StateProvinceID, StateCode = stateCode.StateProvinceCode }
            ));
        }

        public async override Task GetUsStateCodes
        (
            Empty request,
            IServerStreamWriter<StateProvinceCode> responseStream,
            ServerCallContext context
        )
        {
            Result<List<StateCode>> stateCodes = await _sender.Send(new GetStateCodeIdForUSARequest());

            stateCodes.Value.ToList().ForEach(stateCode => responseStream.WriteAsync(
                new StateProvinceCode { Id = stateCode.StateProvinceID, StateCode = stateCode.StateProvinceCode }
            ));
        }
    }
}