using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ChallengeB3.Domain.Models;
using ChallengeB3.Domain.Extesions;
using Microsoft.Extensions.Options;

namespace ChallengeB3.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly QueueSettings _queueSettings;
    public Worker(IOptions<QueueSettings> queueSettings, ILogger<Worker> logger)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Aguardando mensagens...");

        var factory = new ConnectionFactory()
        {
            HostName = _queueSettings.HostName
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;

        channel.BasicConsume(queue: _queueSettings.QueueName, autoAck: true, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(_queueSettings.Interval, stoppingToken);
        }
    }

    private void Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var message = e.Body.ToArray().DeserializeFromByteArrayProtobuf<Register>();

        _logger.LogInformation($"{message.Description} | {message.Status} | {message.Date}");
    }
}