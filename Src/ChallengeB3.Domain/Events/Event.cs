﻿using MediatR;
namespace ChallengeB3.Domain.Events;

public abstract class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }
    protected Event() 
    {
        Timestamp = DateTime.UtcNow;
    }
}
