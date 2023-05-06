using ChallengeB3.Worker;
using System.Runtime.CompilerServices;
using ChallengeB3.Domain.Extesions;

var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(config);
    })
    .ConfigureServices(services =>
    {
        services.AddAppConfiguration(config);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

