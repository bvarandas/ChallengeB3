namespace ChallengeB3.Domain.Interfaces;

public interface IQueueConsumer
{
    Task ExecuteAsync(CancellationToken stoppingToken);
}
