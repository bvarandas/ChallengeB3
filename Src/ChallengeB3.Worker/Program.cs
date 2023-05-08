using ChallengeB3.Worker;
using System.Runtime.CompilerServices;
using ChallengeB3.Domain.Extesions;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Worker.Consumer.Workers;
using ChallengeB3.Application.AutoMapper;
using ChallengeB3.Queue.Worker.Configurations;
using ChallengeB3.Infra.CrossCutting.Ioc;

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
        services.AddHostedService<WorkerCosumer>();
        services.AddHostedService<WorkerProducer>();
        
        services.AddSingleton<IWorkerProducer, WorkerProducer>();
        services.AddAutoMapperSetup();

        NativeInjectorBootStrapper.RegisterServices(services);
    })
    .Build();

await host.RunAsync();

