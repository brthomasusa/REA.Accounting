using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using REA.Accounting.Client.Utilities;
using REA.Accounting.Shared.Models.Organization;
using gRPC.Contracts;

namespace REA.Accounting.Client.UseCases.Organization.GetCompanyDetails.Store
{
    public class GetCompanyDetailEffects : Effect<SetGetCompanyDetailsAction>
    {
        private readonly GrpcChannel? _channel;
        private readonly IMessageService? _messageService;

        public GetCompanyDetailEffects(GrpcChannel channel, IMessageService messageSvc)
            => (_channel, _messageService) = (channel, messageSvc);

        public override async Task HandleAsync
        (
            SetGetCompanyDetailsAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                dispatcher.Dispatch(new SetLoadingFlagAction(true));

                gRPC.Contracts.ItemRequest request = new() { Id = action.CompanyID };
                var client = new CompanyContract.CompanyContractClient(_channel);
                CompanyDetail grpcResponse = await client.GetCompanyDetailByIdAsync(request);
                Console.WriteLine(grpcResponse.CompanyName);

                CompanyDetailModel model = new()
                {
                    Id = grpcResponse.Id,
                    CompanyName = grpcResponse.CompanyName,
                    LegalName = grpcResponse.LegalName,
                    EIN = grpcResponse.Ein,
                    CompanyWebSite = grpcResponse.CompanyWebSite,
                    MailAddressLine1 = grpcResponse.MailAddressLine1,
                    MailAddressLine2 = grpcResponse.MailAddressLine2,
                    MailCity = grpcResponse.MailCity,
                    MailStateProvinceCode = grpcResponse.MailStateProvinceCode,
                    MailPostalCode = grpcResponse.MailPostalCode,
                    DeliveryAddressLine1 = grpcResponse.DeliveryAddressLine1,
                    DeliveryAddressLine2 = grpcResponse.DeliveryAddressLine2,
                    DeliveryCity = grpcResponse.DeliveryCity,
                    DeliveryStateProvinceCode = grpcResponse.DeliveryStateProvinceCode,
                    DeliveryPostalCode = grpcResponse.DeliveryPostalCode,
                    Telephone = grpcResponse.Telephone,
                    Fax = grpcResponse.Fax
                };

                dispatcher.Dispatch(new GetCompanyDetailsSuccessAction(model));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new GetCompanyDetailsFailureMessageAction(Helpers.GetExceptionMessage(ex)));
            }


        }
    }
}