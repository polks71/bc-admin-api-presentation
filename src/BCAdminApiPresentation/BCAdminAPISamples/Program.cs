// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Dynamics.BusinessCentral.AdminCenter;
using Microsoft.Extensions.Caching.Memory;
using BCAdminAPISamples.Helpers;

Console.WriteLine("Hello, World!");
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.AddSimpleConsole();
        logging.AddDebug();
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        var settings = config.Build();
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddUserSecrets(Assembly.GetExecutingAssembly());
    })
    .ConfigureServices((services) =>
    {
        services.AddMemoryCache();
        services.AddTransient<AdminCenterClient> (provider =>
        {
            var cred = new ClientSecretCredential(
                    provider.GetService<IConfiguration>().GetValue<string>("tenantid"),
                    provider.GetService<IConfiguration>().GetValue<string>("appid"),
                    provider.GetService<IConfiguration>().GetValue<string>("appsecret")
            );
            return new AdminCenterClient(cred);
        });
        services.AddTransient<EnvironmentHelper>();
        services.AddTransient<SetAppInsightsKeyHelper>();
        services.AddTransient<UpdateScheduleHelper>();

    }).Build();
var environmentHelper = host.Services.GetRequiredService<EnvironmentHelper>();
await environmentHelper.CreateEnvironmentAsync("Sandbox", "US", "DemoSandbox","","");

var appInsightsKeyHelper = host.Services.GetRequiredService<SetAppInsightsKeyHelper>();
var appInsightsKey = @"InstrumentationKey={REPLACE_WITH_GUID};IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/";
await appInsightsKeyHelper.SetAppInsightsKeyAsync(appInsightsKey, "DemoSandbox");

var updateScheduler = host.Services.GetRequiredService<UpdateScheduleHelper>();
await updateScheduler.ScheduleUpdateAsync("DemoSandbox", new DateTimeOffset(DateTime.Now.AddDays(1)));