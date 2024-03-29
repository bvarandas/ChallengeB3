﻿using ChallengeB3.Domain.Events;
using ChallengeB3.Infra.Data.Context;

namespace ChallengeB3.Infra.Data.Repository.EventSourcing;

public class EventStoreSQLRepository : IEventStoreRepository
{
    private readonly EventStoreSqlContext _context;
    public EventStoreSQLRepository(EventStoreSqlContext context)
    {
        _context = context;
    }
    public IList<StoredEvent> All(int aggregateId)
    {
        return (from e in _context.StoredEvent where e.AggregateId == aggregateId select e).ToList();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void Store(StoredEvent theEvent)
    {
        _context.StoredEvent.Add(theEvent);
        _context.SaveChanges();
    }
}