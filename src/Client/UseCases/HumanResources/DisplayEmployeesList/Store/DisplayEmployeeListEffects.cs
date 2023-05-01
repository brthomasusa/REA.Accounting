using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using MapsterMapper;
using gRPC.Contracts.HumanResources;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.HumanResources;

namespace REA.Accounting.Client.UseCases.HumanResources.DisplayEmployeesList.Store
{
    public class DisplayEmployeeListEffects : Effect<GetEmployeeListAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;
        private readonly IMapper _mapper;

        public DisplayEmployeeListEffects
        (
            GrpcChannel channel,
            IMessageService messageSvc,
            IMapper mapper
        )
            => (_channel, _messageService, _mapper) = (channel, messageSvc, mapper);

        public override async Task HandleAsync
        (
            GetEmployeeListAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                dispatcher.Dispatch(new SetLoadingFlagAction());

                gRPC.Contracts.Shared.grpc_StringSearchTerm parameters = new()
                {
                    Criteria = action.SearchTerm,
                    PageNumber = action.PageNumber,
                    PageSize = action.PageSize
                };

                var client = new EmployeeContract.EmployeeContractClient(_channel);
                grpc_EmployeeListItemsResponse grpcResponse =
                    await client.GetEmployeeListItemsByLastNameAsync(parameters);

                List<EmployeeListItemReadModel> employees = new();
                grpcResponse.GrpcEmployees.ToList()
                                          .ForEach(grpcDept => employees.Add(_mapper.Map<EmployeeListItemReadModel>(grpcDept)));

                MetaData metaData = new()
                {
                    TotalCount = grpcResponse.GrpcMetaData["TotalCount"],
                    PageSize = grpcResponse.GrpcMetaData["PageSize"],
                    CurrentPage = grpcResponse.GrpcMetaData["CurrentPage"],
                    TotalPages = grpcResponse.GrpcMetaData["TotalPages"]
                };

                Console.WriteLine($"Employees: {employees.ToJson()}");
                dispatcher.Dispatch(new GetEmployeeListSuccessAction(employees, metaData, action.SearchTerm));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new GetEmployeeListFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}