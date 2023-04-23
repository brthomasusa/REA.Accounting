using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using MapsterMapper;
using gRPC.Contracts.Organization;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public class DisplayCompanyDepartmentsEffects : Effect<GetDepartmentsAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;
        private readonly IMapper _mapper;

        public DisplayCompanyDepartmentsEffects
        (
            GrpcChannel channel,
            IMessageService messageSvc,
            IMapper mapper
        )
            => (_channel, _messageService, _mapper) = (channel, messageSvc, mapper);

        public override async Task HandleAsync
        (
            GetDepartmentsAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                dispatcher.Dispatch(new SetLoadingFlagAction());

                gRPC.Contracts.Shared.grpc_PagingParameters pagingParameters = new()
                {
                    PageNumber = action.PageNumber,
                    PageSize = action.PageSize
                };

                var client = new CompanyContract.CompanyContractClient(_channel);
                grpc_GetCompanyDepartmentsResponse grpcResponse = await client.GetCompanyDepartmentsAsync(pagingParameters);

                List<DepartmentReadModel> departments = new();
                grpcResponse.GrpcDepartments.ToList()
                                            .ForEach(grpcDept => departments.Add(_mapper.Map<DepartmentReadModel>(grpcDept)));

                MetaData metaData = new()
                {
                    TotalCount = grpcResponse.GrpcMetaData["TotalCount"],
                    PageSize = grpcResponse.GrpcMetaData["PageSize"],
                    CurrentPage = grpcResponse.GrpcMetaData["CurrentPage"],
                    TotalPages = grpcResponse.GrpcMetaData["TotalPages"]
                };

                dispatcher.Dispatch(new GetDepartmentsSuccessAction(departments, metaData));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new GetDepartmentsFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }

        [EffectMethod]
        public async Task GetNextPageOfDataAction
        (
            GetNextPageOfDataAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                gRPC.Contracts.Shared.grpc_PagingParameters pagingParameters = new()
                {
                    PageNumber = action.PageNumber,
                    PageSize = action.PageSize
                };

                var client = new CompanyContract.CompanyContractClient(_channel);
                grpc_GetCompanyDepartmentsResponse grpcResponse = await client.GetCompanyDepartmentsAsync(pagingParameters);

                List<DepartmentReadModel> departments = new();
                grpcResponse.GrpcDepartments.ToList()
                                            .ForEach(grpcDept => departments.Add(_mapper.Map<DepartmentReadModel>(grpcDept)));

                MetaData metaData = new()
                {
                    TotalCount = grpcResponse.GrpcMetaData["TotalCount"],
                    PageSize = grpcResponse.GrpcMetaData["PageSize"],
                    CurrentPage = grpcResponse.GrpcMetaData["CurrentPage"],
                    TotalPages = grpcResponse.GrpcMetaData["TotalPages"]
                };

                dispatcher.Dispatch(new GetNextPageOfDataSuccessAction(departments, metaData));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new GetDepartmentsFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}