using ChallengeB3.Domain.Extesions;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Domain.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChallengeB3.Api.Producer
{
    public class QueueConsumer : IQueueConsumer
    {
        private readonly ILogger<QueueConsumer> _logger;
        private readonly QueueEventSettings _queueSettings;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public QueueConsumer(IOptions<QueueEventSettings> queueSettings, ILogger<QueueConsumer> logger)
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

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando mensagens Event...");
            
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

            }
            catch (Exception ex)
            {
                _channel.BasicNack(e.DeliveryTag, false, true);
            }
        }
    }
}
