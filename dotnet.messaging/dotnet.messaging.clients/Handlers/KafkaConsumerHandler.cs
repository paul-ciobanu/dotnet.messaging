using Confluent.Kafka;
using dotnet.messaging.domain;
using dotnet.messaging.domain.Cache;
using Microsoft.Extensions.Hosting;

namespace dotnet.messaging.clients.Handlers
{
    public class KafkaConsumerHandler : IHostedService
    {
        private readonly string _topic = "simpletalk_topic";

        private readonly IConsumer<Ignore, Message> _consumer;
        private readonly IMessageWriteCache _messageWriter;

        public KafkaConsumerHandler(IMessageWriteCache messageWriter)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            var builder = new ConsumerBuilder<Ignore, Message>(conf);
            builder.SetValueDeserializer(new MessageSerializer());
            _consumer = builder.Build();
            _consumer.Subscribe(_topic);

            _messageWriter = messageWriter;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => ConsumeMessages(cancellationToken), cancellationToken);
            return Task.CompletedTask;
        }

        private void ConsumeMessages(CancellationToken cancellationToken)
        {
            while (true)
            {
                var result = _consumer.Consume(cancellationToken);
                _messageWriter.Add(result.Message.Value);
                Console.WriteLine($"Message: {result.Message.Value} received from {result.TopicPartitionOffset}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
