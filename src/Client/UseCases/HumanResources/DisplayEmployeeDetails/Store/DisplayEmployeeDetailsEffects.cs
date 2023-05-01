using Blazorise;
using Fluxor;
using gRPC.Contracts.HumanResources;
using Grpc.Net.Client;
using MapsterMapper;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeeDetails.Store
{
    public class DisplayEmployeeDetailsEffects : Effect<GetEmployeeDetailsAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;
        private readonly IMapper _mapper;

        public DisplayEmployeeDetailsEffects
        (
            GrpcChannel channel,
            IMessageService messageSvc,
            IMapper mapper
        )
            => (_channel, _messageService, _mapper) = (channel, messageSvc, mapper);

        public override async Task HandleAsync
        (
            GetEmployeeDetailsAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                dispatcher.Dispatch(new SetLoadingFlagAction());

                gRPC.Contracts.Shared.ItemRequest request = new() { Id = action.EmployeeID };
                var client = new EmployeeContract.EmployeeContractClient(_channel);
                grpc_EmployeeDetailResponse grpcResponse = await client.GetEmployeeDetailsByIdWithAllInfoAsync(request);

                EmployeeDetailReadModel model = _mapper.Map<EmployeeDetailReadModel>(grpcResponse);

                dispatcher.Dispatch(new GetEmployeeDetailsSuccessAction(model));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new GetEmployeeDetailsFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}