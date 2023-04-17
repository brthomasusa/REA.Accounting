using Grpc.Core;
using Grpc.Net.Client;
using Empty = Google.Protobuf.WellKnownTypes.Empty;
using Mapster;
using System.Reflection;

using REA.Accounting.Shared.Models.Organization;
using REA.Accounting.Shared.Models.Lookups;
using gRPC.Contracts;
using gRPC.Contracts.Organization;

namespace REA.Accounting.Client.Utilities
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection _)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

            // TypeAdapterConfig<CompanyCommand, CompanyCommandModel>.NewConfig().NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            // TypeAdapterConfig<CompanyDetail, CompanyReadModel>.NewConfig().NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);        
            // TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);            
        }
    }
}