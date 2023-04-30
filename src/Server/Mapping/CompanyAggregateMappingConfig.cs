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
                .Map(dest => dest.ModifiedDate, src => src.ModifiedDate.ToDateTime().ToLocalTime());

            config.NewConfig<CompanyCommand, UpdateCompanyCommand>()
                .Map(dest => dest.CompanyID, src => src.Id);

            config.NewConfig<GetCompanyDetailByIdResponse, CompanyDetail>()
                .Map(dest => dest.Id, src => src.CompanyID)
                .Map(dest => dest.MailAddressLine2, src => string.IsNullOrEmpty(src.MailAddressLine2) ? string.Empty : src.MailAddressLine2)
                .Map(dest => dest.DeliveryAddressLine2, src => string.IsNullOrEmpty(src.DeliveryAddressLine2) ? string.Empty : src.DeliveryAddressLine2);

            config.NewConfig<GetCompanyCommandByIdResponse, CompanyCommand>()
                .Map(dest => dest.Id, src => src.CompanyID)
                .Map(dest => dest.MailAddressLine2, src => string.IsNullOrEmpty(src.MailAddressLine2) ? string.Empty : src.MailAddressLine2)
                .Map(dest => dest.DeliveryAddressLine2, src => string.IsNullOrEmpty(src.DeliveryAddressLine2) ? string.Empty : src.DeliveryAddressLine2);

            config.NewConfig<GetCompanyDepartmentsResponse, grpc_Department>()
                .Map(dest => dest.ModifiedDate, src => GoogleDateTime.FromDateTimeOffset(src.ModifiedDate));

            config.NewConfig<GetCompanyShiftsResponse, grpc_Shift>()
                .Map(dest => dest.ModifiedDate, src => GoogleDateTime.FromDateTimeOffset(src.ModifiedDate));
        }
    }
}