using System.Text.Json;
using Confluent.Kafka;
using dotnet.messaging.domain;

namespace dotnet.messaging.clients.Handlers
{
    public class KafkaProducerHandler : IMessageProducer
    {
        private readonly ProducerConfig _config = new() { BootstrapServers = "localhost:9092" };
        private readonly string _topic = "simpletalk_topic";


        public async Task Send(string message)
        {
            var data = new Message("kafka", message);
            var producerBuilder = new ProducerBuilder<Null, Message>(_config);
            producerBuilder.SetValueSerializer(new MessageSerializer());
            using var producer = producerBuilder.Build();

            var result = await producer.ProduceAsync(_topic, new Message<Null, Message> { Value = data });
            Console.WriteLine($"Message: {result.Message.Value}");
        }
    }
}
