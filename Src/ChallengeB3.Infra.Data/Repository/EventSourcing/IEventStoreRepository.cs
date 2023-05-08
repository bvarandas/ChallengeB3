using ChallengeB3.Domain.Events;
namespace ChallengeB3.Infra.Data.Repository.EventSourcing;
public interface IEventStoreRepository : IDisposable
{
    void Store(StoredEvent theEvent);
    IList<StoredEvent> All(int aggregateId);
}
