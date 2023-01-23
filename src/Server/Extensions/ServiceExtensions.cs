using Microsoft.EntityFrameworkCore;
using REA.Accounting.Infrastructure.Persistence;
using REA.Accounting.Infrastructure.Persistence.Interfaces;
using REA.Accounting.Infrastructure.Persistence.Repositories;
using REA.Accounting.SharedKernel.Interfaces;

namespace REA.Accounting.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "validation-errors-text"));
            });

        public static void ConfigureEfCoreDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EfCoreContext>(options =>
                options.UseSqlServer(
                    configuration["ConnectionStrings:DefaultConnection"],
                    msSqlOptions => msSqlOptions.MigrationsAssembly(typeof(EfCoreContext).Assembly.FullName)
                )
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );
        }

        public static void ConfigureDapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>(s => new DapperContext(configuration["ConnectionStrings:DefaultConnection"]));
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IWriteRepositoryManager, WriteRepositoryManager>();
        }
    }
}