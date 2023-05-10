using ChallengeB3.Worker;
using System.Runtime.CompilerServices;
using ChallengeB3.Domain.Extesions;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Worker.Consumer.Workers;
using ChallengeB3.Application.AutoMapper;
using ChallengeB3.Queue.Worker.Configurations;
using ChallengeB3.Infra.CrossCutting.Ioc;
using System.Reflection;
using ChallengeB3.Application.Services;
using ChallengeB3.Infra.Data.Repository;
using ChallengeB3.Infra.Data.Context;
using ChallengeB3.Infra.Data.UoW;
using ChallengeB3.Domain.Bus;
using ChallengeB3.Domain.CommandHandlers;
using ChallengeB3.Domain.Commands;
using ChallengeB3.Domain.EventHandlers;
using ChallengeB3.Domain.Events;
using ChallengeB3.Domain.Notifications;
using ChallengeB3.Infra.CrossCutting.Bus;
using ChallengeB3.Infra.Data.EventSourcing;
using ChallengeB3.Infra.Data.Repository.EventSourcing;
using MediatR;

var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

try
{
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.Sources.Clear();
            builder.AddConfiguration(config);

        })
        .ConfigureServices(services =>
        {
            services.AddAppConfiguration(config);

            // Domain Bus (Mediator)
            services.AddSingleton<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddSingleton<IRegisterService, RegisterService>();

            // Domain - Events
            services.AddSingleton<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddSingleton<INotificationHandler<RegisterInsertedEvent>, RegisterEventHandler>();
            services.AddSingleton<INotificationHandler<RegisterUpdatedEvent>, RegisterEventHandler>();
            services.AddSingleton<INotificationHandler<RegisterRemovedEvent>, RegisterEventHandler>();

            // Domain - Commands
            services.AddSingleton<IRequestHandler<InsertRegisterCommand, bool>, RegisterCommandHandler>();
            services.AddSingleton<IRequestHandler<UpdateRegisterCommand, bool>, RegisterCommandHandler>();
            services.AddSingleton<IRequestHandler<RemoveRegisterCommand, bool>, RegisterCommandHandler>();

            // Infra - Data
            services.AddSingleton<IRegisterRepository, RegisterRepository>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<DbContextClass>();

            // Infra - Data EventSourcing
            services.AddSingleton<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddSingleton<IEventStore, SqlEventStore>();
            services.AddSingleton<EventStoreSqlContext>();

            services.AddSingleton<IWorkerProducer, WorkerProducer>();

            services.AddHostedService<WorkerCosumer>();
            services.AddHostedService<WorkerProducer>();

            
            //services.AddSingleton<IWorkerConsumer, WorkerCosumer>();
            services.AddAutoMapperSetup();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            
            //NativeInjectorBootStrapper.RegisterServices(services);
        })
        .Build();
    await host.RunAsync();
}
catch(Exception ex)
{
    
}


