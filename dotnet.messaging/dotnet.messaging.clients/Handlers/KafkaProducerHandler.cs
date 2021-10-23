using Confluent.Kafka;

namespace dotnet.messaging.clients.Handlers
{
    public class KafkaProducerHandler
    {
        private readonly ProducerConfig _config = new() { BootstrapServers = "localhost:9092" };
        private readonly string _topic = "simpletalk_topic";

        public async Task Send(string message)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }
}
