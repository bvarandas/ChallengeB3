using ChallengeB3.Domain.Models;

namespace ChallengeB3.Domain.Interfaces;

public interface IQueueProducer
{
    void PublishMessage(Register message);
}
