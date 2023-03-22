using Carter;
using FluentValidation;
using MediatR;
using NLog;
using NLog.Web;

using REA.Accounting.Application;
using REA.Accounting.Application.Behaviors;
using REA.Accounting.Server.Extensions;
using REA.Accounting.Server.Middleware;
using REA.Accounting.Server.Contracts;
using REA.Accounting.Application.HumanResources.CreateEmployee;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
GlobalDiagnosticsContext.Set("logDirectory", Directory.GetCurrentDirectory());

logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddMediatR(ApplicationAssembly.Instance);
    builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandDataValidator>();
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(BusinessRulesValidationBehavior<,>));
    builder.Services.AddPipelineBehaviorServices();

    builder.Services.AddCarter();
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    // Add services from namespace Server.Extensions to the container.
    builder.Services.ConfigureCors();
    builder.Services.AddInfrastructureServices();
    builder.Services.ConfigureEfCoreDbContext(builder.Configuration);
    builder.Services.ConfigureDapper(builder.Configuration);
    builder.Services.AddRepositoryServices();
    builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
    builder.Services.AddGrpc();
    builder.Services.AddGrpcReflection();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
    app.MapGrpcReflectionService();
    app.MapGrpcService<CompanyContractService>().RequireCors("AllowAll");

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");
    app.MapCarter();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

namespace REA.Accounting.Server
{
    public partial class Program { }
}
