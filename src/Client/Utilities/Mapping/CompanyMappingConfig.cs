using Mapster;
using gRPC.Contracts.Organization;
using REA.Accounting.Shared.Models.Organization;

namespace REA.Accounting.Client.Utilities.Mapping
{
    public sealed class CompanyMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // TypeAdapterConfig<TSource, TDestination>
            config.NewConfig<grpc_Department, DepartmentReadModel>()
                .Map(dest => dest.DepartmentID, src => src.DepartmentId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.GroupName, src => src.GroupName)
                .Map(dest => dest.ModifiedDate, src => src.ModifiedDate.ToDateTime());
        }
    }
}