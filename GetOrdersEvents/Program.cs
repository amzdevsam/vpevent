using GetOrdersEvents.Function.Domain.Services;
using GetOrdersEvents.Function.Domain.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var host = new HostBuilder()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
        var fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
        string path = fileInfo.Directory.Parent.FullName;

        string _environmentName = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
        configurationBuilder
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appsettings.{_environmentName}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults(worker =>
    {
        worker.Services.AddScoped<IFtpService, FtpService>();
        worker.Services.AddOptions<SftpSettings>().Configure<IConfiguration>((settings, configuration) =>
        {
            configuration.Bind("SftpSettings", settings);
        });
        worker.UseNewtonsoftJson();
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
