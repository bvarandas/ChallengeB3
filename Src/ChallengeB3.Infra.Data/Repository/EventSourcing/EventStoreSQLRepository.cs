using ChallengeB3.Domain.Events;
namespace ChallengeB3.Infra.Data.Repository.EventSourcing;

public class EventStoreSQLRepository : IEventStoreRepository
{

    public IList<StoredEvent> All(int aggregateId)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void Store(StoredEvent theEvent)
    {
        throw new NotImplementedException();
    }
}
