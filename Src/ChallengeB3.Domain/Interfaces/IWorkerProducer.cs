using ChallengeB3.Domain.Models;
namespace ChallengeB3.Domain.Interfaces;

public interface IWorkerProducer
{
    Task PublishMessage(Register message);
}
