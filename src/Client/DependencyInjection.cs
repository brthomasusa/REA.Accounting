
using REA.Accounting.Client.Utilities.Mapping;

namespace REA.Accounting.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureExternalLibraries(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddBlazorise();
            services.AddFluentValidation();
            services.AddFluxor();

            return services;
        }
    }
}