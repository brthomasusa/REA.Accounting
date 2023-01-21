using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using REA.Accounting.Server;

namespace REA.Accounting.IntegrationTests.Base
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IConfiguration? Configuration { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("integrationsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                config.AddConfiguration(Configuration);
            });

            // Is to be called after the `ConfigureServices` from the Startup
            // which allows you to overwrite the DI with mocked instances
            builder.ConfigureTestServices(services =>
            {
                // services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigMock>();
            });
        }
    }
}