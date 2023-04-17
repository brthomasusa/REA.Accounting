using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using Empty = Google.Protobuf.WellKnownTypes.Empty;
// using Mapster;
using MapsterMapper;
using gRPC.Contracts.Lookups;
using gRPC.Contracts.Shared;
using gRPC.Contracts.Organization;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store;
using REA.Accounting.Shared.Models.Organization;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Client.UseCases.Organization.UpdateCompanyDetails.Store
{
    public class UpdateCompanyDetailsEffects : Effect<LoadCompanyDetailsForEditingAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;
        private readonly IMapper _mapper;

        public UpdateCompanyDetailsEffects
        (
            GrpcChannel channel,
            IMessageService messageSvc,
            IMapper mapper
        )
            => (_channel, _messageService, _mapper) = (channel, messageSvc, mapper);


        [EffectMethod(typeof(LoadStateCodesAction))]
        public async Task LoadStateCodes(IDispatcher dispatcher)
        {
            try
            {
                var client = new LookupsContract.LookupsContractClient(_channel);
                var stream = client.GetUsStateCodes(new Empty()).ResponseStream;

                List<StateCode> stateCodes = new();

                while (await stream.MoveNext(default))
                {
                    StateProvinceCode code = (StateProvinceCode)stream.Current;
                    stateCodes.Add(_mapper.Map<StateCode>(code));
                }

                dispatcher.Dispatch(new LoadStateCodesSuccessAction(stateCodes));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new LoadStateCodesFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }

        public override async Task HandleAsync
        (
            LoadCompanyDetailsForEditingAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                dispatcher.Dispatch(new SetLoadingFlagAction());

                var client = new CompanyContract.CompanyContractClient(_channel);
                ItemRequest request = new() { Id = action.CompanyID };
                CompanyCommand grpcResponse = await client.GetCompanyCommandByIdAsync(request);

                CompanyCommandModel model = _mapper.Map<CompanyCommandModel>(grpcResponse);

                dispatcher.Dispatch(new UpdateCompanyDetailsInitializeSuccessAction(model));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new UpdateCompanyDetailsInitializeFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }

        [EffectMethod]
        public async Task SubmitUpdatedCompanyCommandModel
        (
            UpdateCompanyDetailsSubmitAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                var client = new CompanyContract.CompanyContractClient(_channel);

                CompanyCommand cmd = _mapper.Map<CompanyCommand>(action.CommandModel);
                GenericResponse response = await client.UpdateAsync(cmd);

                if (response.Success)
                {
                    dispatcher.Dispatch(new UpdateCompanyDetailsSubmitSuccessAction());
                    dispatcher.Dispatch(new SetDisplayInitializeFlagAction(false));
                }
                else
                {
                    dispatcher.Dispatch(new UpdateCompanyDetailsSubmitFailureAction("Update failed! Server Error."));
                }
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new UpdateCompanyDetailsSubmitFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}