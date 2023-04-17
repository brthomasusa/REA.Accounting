using Mapster;
using gRPC.Contracts.Lookups;
using REA.Accounting.Shared.Models.Lookups;

namespace REA.Accounting.Client.Utilities.Mapping
{
    public sealed class LookupsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<StateProvinceCode, StateCode>()
                .Map(dest => dest.StateProvinceID, src => src.Id)
                .Map(dest => dest.StateProvinceCode, src => src.StateCode);
        }
    }
}