using Election.Management.Repository;
using Election.Management.System.Interface;
using Election.Management.System.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddTransient<IElectionManagementSystemRepository, ElectionManagementSystemRepository>();
        services.AddTransient<IElectionManagementSystemService,ElectionManagementSystemService>();
    })
    .Build();

host.Run();
