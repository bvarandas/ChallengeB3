using ChallengeB3.Domain.Events;
using ChallengeB3.Infra.Data.Repository.EventSourcing;
using Newtonsoft.Json;

namespace ChallengeB3.Infra.Data.EventSourcing;
public class SqlEventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    public SqlEventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public void Save<T>(T theEvent) where T : Event
    {
        var serializedData = JsonConvert.SerializeObject(theEvent);

        var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                "bvarandas");

        _eventStoreRepository.Store(storedEvent);
    }
}
