using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ChallengeB3.Domain.Models;
using ChallengeB3.Domain.Extesions;
using Microsoft.Extensions.Options;

namespace ChallengeB3.Worker.Consumer.Workers;

public class WorkerCosumer : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly ILogger<WorkerCosumer> _logger;
    private readonly QueueCommandSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public WorkerCosumer(IOptions<QueueCommandSettings> queueSettings, ILogger<WorkerCosumer> logger)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
        _factory = new ConnectionFactory()
        {
            HostName = _queueSettings.HostName
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Aguardando mensagens Command...");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_Received;

        _channel.BasicConsume(queue: _queueSettings.QueueName, autoAck: false, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(_queueSettings.Interval, stoppingToken);
        }
    }

    private void Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        try
        {
            var message = e.Body.ToArray().DeserializeFromByteArrayProtobuf<Register>();
            _logger.LogInformation($"{message.Description} | {message.Status} | {message.Date}");


            _channel.BasicAck(e.DeliveryTag, false);

        }catch (Exception ex) 
        {
            _channel.BasicNack(e.DeliveryTag, false, true);
        }
    }
}