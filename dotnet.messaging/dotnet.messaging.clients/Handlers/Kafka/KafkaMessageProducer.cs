using System.Text.Json;
using Confluent.Kafka;
using dotnet.messaging.domain;

namespace dotnet.messaging.clients.Handlers.Kafka
{
    public class KafkaMessageProducer : IMessageProducer
    {
        private readonly ProducerConfig _config = new() { BootstrapServers = "localhost:9092" };
        private readonly string _topic = "simpletalktopic";
        private readonly IProducer<Null, Message> _producer;

        public KafkaMessageProducer()
        {
            var producerBuilder = new ProducerBuilder<Null, Message>(_config);
            producerBuilder.SetValueSerializer(new MessageSerializer());
            _producer = producerBuilder.Build();
        }


        public async Task Send(string message)
        {
            var data = new Message("kafka", message);

            try
            {
                var result = await _producer.ProduceAsync(_topic, new Message<Null, Message> { Value = data });
                Console.WriteLine($"Kafka message: {result.Message.Value}");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
