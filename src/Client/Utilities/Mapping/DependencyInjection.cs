using System.Reflection;
using Mapster;
using MapsterMapper;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FluentValidation;
using Fluxor;

namespace REA.Accounting.Client.Utilities.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            config.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }

        public static IServiceCollection AddBlazorise(this IServiceCollection services)
        {
            services
              .AddBlazorise(options => options.Immediate = true)
              .AddBootstrap5Providers()
              .AddFontAwesomeIcons();

            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services
              .AddValidatorsFromAssembly(typeof(App).Assembly)
              .AddFluentValidationHandler();

            return services;
        }

        public static IServiceCollection AddFluxor(this IServiceCollection services)
        {
            var currentAssembly = typeof(Program).Assembly;
            services.AddFluxor(options =>
            {
                options.ScanAssemblies(currentAssembly);
#if DEBUG
                options.UseReduxDevTools();
#endif
            });

            return services;
        }
    }
}