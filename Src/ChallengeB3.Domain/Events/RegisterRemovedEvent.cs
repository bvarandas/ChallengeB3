namespace ChallengeB3.Domain.Events;

public class RegisterRemovedEvent : Event
{
    public int RegisterId { get; set; }
    public RegisterRemovedEvent(int registerId)
    {
        RegisterId = registerId;
    }
}
