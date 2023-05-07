using ChallengeB3.Domain.Interfaces;
using RabbitMQ.Client;
using ChallengeB3.Domain.Extesions;
using ChallengeB3.Domain.Models;
using Microsoft.Extensions.Options;
namespace ChallengeB3.Api.Producer;

public class QueueProducer : IQueueProducer
{
    private readonly QueueCommandSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly ILogger<QueueProducer> _logger;
    public QueueProducer(IOptions<QueueCommandSettings> queueSettings, ILogger<QueueProducer> logger)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
        _factory = new ConnectionFactory { HostName = _queueSettings.HostName };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public Task PublishMessage(Register message)
    {
        try
        {
            var body = message.SerializeToByteArrayProtobuf();

            _channel.QueueDeclare(
            queue: _queueSettings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueSettings.QueueName,
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return Task.FromException(ex);
        }
    }
}
