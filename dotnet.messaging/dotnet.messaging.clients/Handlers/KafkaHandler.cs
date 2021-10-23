using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace dotnet.messaging.clients.Handlers
{
    public class KafkaHandler : IHostedService
    {
        private readonly string _topic = "simpletalk_topic";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var cancelToken = new CancellationTokenSource();
            using var builder = new ConsumerBuilder<Ignore, string>(conf).Build();
            builder.Subscribe(_topic);

            while (true)
            {
                var consumer = builder.Consume(cancelToken.Token);
                Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
