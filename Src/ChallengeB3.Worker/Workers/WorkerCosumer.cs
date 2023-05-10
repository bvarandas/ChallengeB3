using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ChallengeB3.Domain.Models;
using ChallengeB3.Domain.Extesions;
using Microsoft.Extensions.Options;
using ChallengeB3.Domain.CommandHandlers;
using ChallengeB3.Domain.Bus;
using ChallengeB3.Domain.Commands;
using Microsoft.EntityFrameworkCore.Internal;
using ChallengeB3.Domain.Interfaces;

namespace ChallengeB3.Worker.Consumer.Workers;

public class WorkerCosumer : BackgroundService
{
    private IRegisterService _registerService;
    private readonly ILogger<WorkerCosumer> _logger;
    private readonly QueueCommandSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public WorkerCosumer(IOptions<QueueCommandSettings> queueSettings, 
        ILogger<WorkerCosumer> logger, IRegisterService registerService)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
        _factory = new ConnectionFactory()
        {
            HostName = _queueSettings.HostName
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _registerService = registerService;
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

            switch(message.Action)
            {
                case "insert":
                    _registerService.AddRegister
                    _bus.SendCommand<InsertRegisterCommand>(message);
                    break;
                case "update":
                    break;
                case "remove":
                    break;
            }

            _channel.BasicAck(e.DeliveryTag, false);

        }catch (Exception ex) 
        {
            _channel.BasicNack(e.DeliveryTag, false, true);
        }
    }
}