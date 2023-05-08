using Microsoft.Extensions.DependencyInjection;
using ChallengeB3.Infra.Data.UoW;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Application.Services;
using MediatR;
using ChallengeB3.Domain.Notifications;
using ChallengeB3.Domain.EventHandlers;
using ChallengeB3.Domain.Events;
using ChallengeB3.Domain.Bus;
using ChallengeB3.Infra.CrossCutting.Bus;
using ChallengeB3.Infra.Data.Context;
using ChallengeB3.Infra.Data.EventSourcing;
using ChallengeB3.Infra.Data.Repository.EventSourcing;
using ChallengeB3.Domain.CommandHandlers;
using ChallengeB3.Domain.Commands;

namespace ChallengeB3.Infra.CrossCutting.Ioc;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Asp .NET HttpContext dependency
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Domain Bus (Mediator)
        services.AddScoped<IMediatorHandler, InMemoryBus>();

        // Application
        services.AddScoped<IRegisterService, RegisterService>();

        // Domain - Events
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        services.AddScoped<INotificationHandler<RegisterInsertedEvent>, RegisterEventHandler>();
        services.AddScoped<INotificationHandler<RegisterUpdatedEvent>, RegisterEventHandler>();
        services.AddScoped<INotificationHandler<RegisterRemovedEvent>, RegisterEventHandler>();

        // Domain - Commands
        services.AddScoped<IRequestHandler<InsertRegisterCommand, bool>, RegisterCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRegisterCommand, bool>, RegisterCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveRegisterCommand, bool>, RegisterCommandHandler>();

        // Infra - Data
        services.AddScoped<IRegisterRepository, IRegisterRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DbContextClass>();

        // Infra - Data EventSourcing
        services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
        services.AddScoped<IEventStore, SqlEventStore>();
        services.AddScoped<EventStoreSqlContext>();

    }
}
