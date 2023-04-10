using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FluentValidation;
using Fluxor;

using REA.Accounting.Client;
using REA.Accounting.Client.Utilities;

var currentAssembly = typeof(Program).Assembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
  .AddBlazorise(options => options.Immediate = true)
  .AddBootstrap5Providers()
  .AddFontAwesomeIcons()
  .AddFluentValidationHandler();
builder.Services.AddValidatorsFromAssembly(typeof(App).Assembly);

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(currentAssembly);
#if DEBUG
    options.UseReduxDevTools();
#endif        
});

builder.Services.RegisterMapsterConfiguration();
builder.Services.AddSingleton(services =>
{
    var navigationManager = services.GetRequiredService<NavigationManager>();
    var backendUrl = navigationManager.BaseUri;

    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

    return GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
});

await builder.Build().RunAsync();
