using Blazorise;
using Fluxor;
using Grpc.Net.Client;
using Empty = Google.Protobuf.WellKnownTypes.Empty;
using Mapster;

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

        public UpdateCompanyDetailsEffects
        (
            GrpcChannel channel,
            IMessageService messageSvc
        )
            => (_channel, _messageService) = (channel, messageSvc);


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
                    stateCodes.Add(
                        new StateCode { StateProvinceID = code.Id, StateProvinceCode = code.StateCode }
                    );
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

                CompanyCommandModel model = grpcResponse.Adapt<CompanyCommandModel>();

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
                // CompanyCommand cmd = action.CommandModel.Adapt<CompanyCommand>();

                CompanyCommand cmd = new()
                {
                    Id = action.CommandModel.Id,
                    CompanyName = action.CommandModel.CompanyName,
                    LegalName = action.CommandModel.LegalName,
                    Ein = action.CommandModel.EIN,
                    CompanyWebSite = action.CommandModel.CompanyWebSite,
                    MailAddressLine1 = action.CommandModel.MailAddressLine1,
                    MailAddressLine2 = action.CommandModel.MailAddressLine2,
                    MailCity = action.CommandModel.MailCity,
                    MailStateProvinceID = action.CommandModel.MailStateProvinceID,
                    MailPostalCode = action.CommandModel.MailPostalCode,
                    DeliveryAddressLine1 = action.CommandModel.DeliveryAddressLine1,
                    DeliveryAddressLine2 = action.CommandModel.DeliveryAddressLine2,
                    DeliveryCity = action.CommandModel.DeliveryCity,
                    DeliveryStateProvinceID = action.CommandModel.DeliveryStateProvinceID,
                    DeliveryPostalCode = action.CommandModel.DeliveryPostalCode,
                    Telephone = action.CommandModel.Telephone,
                    Fax = action.CommandModel.Fax
                };

                var client = new CompanyContract.CompanyContractClient(_channel);
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