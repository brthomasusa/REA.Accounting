using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using MapsterMapper;
using gRPC.Contracts.Organization;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDetails.Store
{
    public class DisplayCompanyDetailEffects : Effect<SetDisplayCompanyDetailsAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;
        private readonly IMapper _mapper;

        public DisplayCompanyDetailEffects
        (
            GrpcChannel channel,
            IMessageService messageSvc,
            IMapper mapper
        )
            => (_channel, _messageService, _mapper) = (channel, messageSvc, mapper);

        public override async Task HandleAsync
        (
            SetDisplayCompanyDetailsAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                dispatcher.Dispatch(new SetLoadingFlagAction());

                gRPC.Contracts.Shared.ItemRequest request = new() { Id = action.CompanyID };
                var client = new CompanyContract.CompanyContractClient(_channel);
                CompanyDetail grpcResponse = await client.GetCompanyDetailByIdAsync(request);

                CompanyReadModel model = _mapper.Map<CompanyReadModel>(grpcResponse);

                dispatcher.Dispatch(new DisplayCompanyDetailsSuccessAction(model));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new DisplayCompanyDetailsFailureMessageAction(Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}