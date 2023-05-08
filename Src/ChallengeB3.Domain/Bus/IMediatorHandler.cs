using ChallengeB3.Domain.Commands;
using ChallengeB3.Domain.Events;

namespace ChallengeB3.Domain.Bus;

public interface IMediatorHandler
{
    Task SendCommand<T>(T command) where T : Command;
    Task RaiseEvent<T>(T @event) where T : Event;
}
