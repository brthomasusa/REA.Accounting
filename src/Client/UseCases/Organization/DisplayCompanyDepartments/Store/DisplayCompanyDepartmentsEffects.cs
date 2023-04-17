using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using Mapster;
using gRPC.Contracts.Organization;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.UseCases.Organization.DisplayCompanyDepartments.Store
{
    public class DisplayCompanyDepartmentsEffects : Effect<GetDepartmentsAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;

        public DisplayCompanyDepartmentsEffects
        (
            GrpcChannel channel,
            IMessageService messageSvc
        )
            => (_channel, _messageService) = (channel, messageSvc);

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
                                            .ForEach(grpcDept => departments.Add(grpcDept.Adapt<DepartmentReadModel>()));

                dispatcher.Dispatch(new GetDepartmentsSuccessAction(departments));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new GetDepartmentsFailureAction(Helpers.GetExceptionMessage(ex)));
            }
        }
    }
}