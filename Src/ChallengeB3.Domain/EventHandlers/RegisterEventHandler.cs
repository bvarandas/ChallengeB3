using ChallengeB3.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Domain.EventHandlers;

public class RegisterEventHandler :
    INotificationHandler<RegisterInsertedEvent>,
    INotificationHandler<RegisterRemovedEvent>,
    INotificationHandler<RegisterUpdatedEvent>
{
    public Task Handle(RegisterInsertedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(RegisterRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(RegisterUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
