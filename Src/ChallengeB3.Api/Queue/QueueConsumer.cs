using ChallengeB3.Api.Hubs;
using ChallengeB3.Domain.Extesions;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;

namespace ChallengeB3.Api.Producer
{
    public class QueueConsumer :  IQueueConsumer
    {
        private readonly ILogger<QueueConsumer> _logger;
        private readonly QueueEventSettings _queueSettings;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<int, Register> _registers;
        public QueueConsumer(
            IOptions<QueueEventSettings> queueSettings, 
            ILogger<QueueConsumer> logger,
            IServiceProvider provider)
        {
            _logger = logger;
            _queueSettings = queueSettings.Value;
            _factory = new ConnectionFactory()
            {
                HostName = _queueSettings.HostName
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _serviceProvider = provider;
            _registers = new Dictionary<int, Register>();
        }

        public  async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando mensagens Event...");
            
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            try
            {
                _channel.BasicConsume(queue: _queueSettings.QueueName, autoAck: false, consumer: consumer);
            }catch(Exception ex)
            {

            }
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(_queueSettings.Interval, stoppingToken);
            }

        }

        public Register RegisterGetById(int registerId)
        {
            return _registers[registerId];
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var messageList = e.Body.ToArray().DeserializeFromByteArrayProtobuf<List<Register>>();


                using (var scope = _serviceProvider.CreateScope())
                {
                    var hubContext = scope.ServiceProvider
                        .GetRequiredService<IHubContext<BrokerHub>>();

                    _registers.Clear();
                    messageList.ForEach(mess => {
                        _registers.TryAdd(mess.RegisterId, mess);
                    });
                    
                    hubContext.Clients.Group("CrudMessage").SendAsync("ReceiveMessage", messageList);
                }

                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var messageList = e.Body.ToArray().DeserializeFromByteArrayProtobuf<Register>();
                if (messageList is Register)
                {
                    _channel.BasicAck(e.DeliveryTag, false);
                }else
                _channel.BasicNack(e.DeliveryTag, false, true);
            }
        }
    }
}
