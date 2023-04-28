using Mapster;
using GoogleDateTime = Google.Protobuf.WellKnownTypes.Timestamp;
using gRPC.Contracts.Organization;
using REA.Accounting.Application.Organization.UpdateCompany;
using REA.Accounting.Shared.Models.Organization;
using REA.Accounting.Infrastructure.Persistence.Queries.Organization;

namespace REA.Accounting.Server.Mapping
{
    public sealed class CompanyAggregateMappingConfig : IRegister
    {
        void IRegister.Register(TypeAdapterConfig config)
        {
            /*
                    TypeAdapterConfig<TSource, TDestination>   
            */

            config.NewConfig<grpc_Department, DepartmentReadModel>()
                .Map(dest => dest.ModifiedDate, src => src.ModifiedDate.ToDateTime());

            config.NewConfig<CompanyCommand, UpdateCompanyCommand>()
                .Map(dest => dest.CompanyID, src => src.Id);

            config.NewConfig<GetCompanyDetailByIdResponse, CompanyDetail>()
                .Map(dest => dest.Id, src => src.CompanyID);

            config.NewConfig<GetCompanyCommandByIdResponse, CompanyCommand>()
                .Map(dest => dest.Id, src => src.CompanyID);

            config.NewConfig<GetCompanyDepartmentsResponse, grpc_Department>()
                .Map(dest => dest.ModifiedDate, src => GoogleDateTime.FromDateTime(src.ModifiedDate.ToUniversalTime()));

            config.NewConfig<GetCompanyShiftsResponse, grpc_Shift>()
                .Map(dest => dest.ModifiedDate, src => GoogleDateTime.FromDateTime(src.ModifiedDate.ToUniversalTime()));
        }
    }
}