using System.Text.Json;
using Confluent.Kafka;
using dotnet.messaging.domain;
using dotnet.messaging.domain.Cache;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace dotnet.messaging.clients.Handlers.RabbitMQ
{
    public class RabbitMQConsumerHandler : IHostedService
    {
        private readonly IModel _channel;
        private readonly IMessageWriteCache _messageWriter;

        public RabbitMQConsumerHandler(IMessageWriteCache messageWriter)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(
                queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) => ConsumeMessages(ea);
            _channel.BasicConsume(
                queue: "hello",
                autoAck: true,
                consumer: consumer);

            _messageWriter = messageWriter;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void ConsumeMessages(BasicDeliverEventArgs ea)
        {
            var rawData = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<Message>(rawData);
            _messageWriter.Add(message);
            Console.WriteLine($"Message: {message} received from {ea.ConsumerTag}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
