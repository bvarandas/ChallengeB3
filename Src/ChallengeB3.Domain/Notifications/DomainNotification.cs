﻿using ChallengeB3.Domain.Events;

namespace ChallengeB3.Domain.Notifications;

public class DomainNotification : Event
{
    public int DomainNotificationId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public int Version { get; private set; }

    public DomainNotification(string key, string value)
    {
        DomainNotificationId = 0;
        Version = 1;
        Key = key;
        Value = value;
    }

}
