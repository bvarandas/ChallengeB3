using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ChallengeB3.Domain.Models;
using Microsoft.Extensions.Options;
using ChallengeB3.Domain.Extesions;
using System.Threading.Channels;
using ChallengeB3.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace ChallengeB3.Worker.Consumer.Workers;

public class WorkerProducer : BackgroundService, IWorkerProducer
{
    private readonly ILogger<WorkerProducer> _logger;
    private readonly QueueEventSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    public WorkerProducer(IOptions<QueueEventSettings> queueSettings, ILogger<WorkerProducer> logger)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
        _factory = new ConnectionFactory { HostName = _queueSettings.HostName };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public async Task PublishMessage(Register message)
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
            
        }
        catch (Exception ex)
        {
            //_channel.
            _logger.LogError(ex, $"{ex.Message}");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(_queueSettings.Interval, stoppingToken);
        }
    }
}