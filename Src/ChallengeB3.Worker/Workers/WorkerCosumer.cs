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

public class WorkerConsumer : BackgroundService, IWorkerConsumer
{
    private readonly IWorkerProducer _workerProducer;
    private readonly IRegisterService _registerService;
    private readonly ILogger<WorkerConsumer> _logger;
    private readonly QueueCommandSettings _queueSettings;
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    
    public WorkerConsumer(IOptions<QueueCommandSettings> queueSettings, 
        ILogger<WorkerConsumer> logger, IRegisterService registerService,
        IWorkerProducer workerProducer)
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
        _workerProducer = workerProducer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Aguardando mensagens Command...");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_Received;

        _channel.BasicConsume(queue: _queueSettings.QueueName, autoAck: false, consumer: consumer);


        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(_queueSettings.Interval, stoppingToken);
        }
    }

    private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        try
        {
            var message = e.Body.ToArray().DeserializeFromByteArrayProtobuf<Register>();
            _logger.LogInformation($"{message.Description} | {message.Status} | {message.Date}");

            switch(message.Action)
            {
                case "insert":
                    await _registerService.AddRegisterAsync(message);
                    break;

                case "update":
                    await _registerService.UpdateRegisterAsync(message);
                    break;

                case "remove":
                    _registerService.RemoveRegisterAsync(message.RegisterId);
                    break;

                case "getall":
                    var registerlist = await _registerService.GetListAllAsync();
                    await WorkerProducer._Singleton.PublishMessages(registerlist.ToList());
                    break;

                case "get":
                    var register = await _registerService.GetRegisterByIDAsync(message.RegisterId);
                    var list = new List<Register>();
                    list.Add(register);
                    await WorkerProducer._Singleton.PublishMessages(list);
                    break;

            }

            _channel.BasicAck(e.DeliveryTag, false);

        }catch (Exception ex) 
        {
            _channel.BasicNack(e.DeliveryTag, false, true);
        }
    }
}