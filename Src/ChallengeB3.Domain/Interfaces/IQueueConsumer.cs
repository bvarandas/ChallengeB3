using ChallengeB3.Domain.Models;

namespace ChallengeB3.Domain.Interfaces;

public interface IQueueConsumer
{
    Task ExecuteAsync(CancellationToken stoppingToken=default);
    Register RegisterGetById(int registerId);
}
