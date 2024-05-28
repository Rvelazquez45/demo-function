using DemoFunction.Logic;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration
    (
        configuration =>
        {// The order matters. The last one that exists wins.
            configuration.AddJsonFile(path: "appsettings.json", optional: true);
            configuration.AddJsonFile(path: "local.settings.json", optional: true);
            configuration.AddEnvironmentVariables();
        }
    )
    .ConfigureServices
    (
        services =>
        {
            services.AddApplicationInsightsTelemetryWorkerService();
            services.ConfigureFunctionsApplicationInsights();

            services.AddSingleton<ISampleLogic, SampleLogic>();
        }
    ).Build();

host.Run();
