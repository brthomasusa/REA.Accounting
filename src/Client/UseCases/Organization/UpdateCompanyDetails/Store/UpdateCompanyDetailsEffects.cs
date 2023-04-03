using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using Empty = Google.Protobuf.WellKnownTypes.Empty;
using Mapster;

using gRPC.Contracts;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public class UpdateCompanyDetailsEffects : Effect<GetCompanyDetailsForEditingAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;

        public UpdateCompanyDetailsEffects(GrpcChannel channel, IMessageService messageSvc)
            => (_channel, _messageService) = (channel, messageSvc);

        public override async Task HandleAsync
        (
            GetCompanyDetailsForEditingAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                dispatcher.Dispatch(new SetLoadingFlagAction());

                List<StateCode> stateCodes = await FetchStateCodes();

                var client = new CompanyContract.CompanyContractClient(_channel);
                gRPC.Contracts.ItemRequest request = new() { Id = action.CompanyID };
                CompanyCommand grpcResponse = await client.GetCompanyCommandByIdAsync(request);

                CompanyCommandModel model = grpcResponse.Adapt<CompanyCommandModel>();

                dispatcher.Dispatch(new UpdateCompanyDetailsInitializeSuccessAction(stateCodes, model));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new UpdateCompanyDetailsInitializeFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }

        private async Task<List<StateCode>> FetchStateCodes()
        {
            var client = new LookupsContract.LookupsContractClient(_channel);
            var stream = client.GetUsStateCodes(new Empty()).ResponseStream;

            List<StateCode> stateCodes = new();

            while (await stream.MoveNext(default))
            {
                StateProvinceCode code = (StateProvinceCode)stream.Current;
                stateCodes.Add(
                    new StateCode { StateProvinceID = code.Id, StateProvinceCode = code.StateCode }
                );
            }

            return stateCodes;
        }
    }
}