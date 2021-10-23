using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using dotnet.messaging.domain;
using RabbitMQ.Client;

namespace dotnet.messaging.clients.Handlers.RabbitMQ
{
    internal class RabbitMQMessageProducer : IMessageProducer
    {
        private readonly IModel _channel;

        public RabbitMQMessageProducer()
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
        }

        public Task Send(string message)
        {
            var data = new Message("rabbitMQ", message);
            var body = JsonSerializer.SerializeToUtf8Bytes(data);

            _channel.BasicPublish(
                exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);

            Console.WriteLine($"RabbitMQ message: {data}");
            return Task.CompletedTask;
        }
    }
}
