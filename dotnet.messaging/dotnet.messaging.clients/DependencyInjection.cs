using dotnet.messaging.clients.Handlers;
using dotnet.messaging.clients.Handlers.Kafka;
using dotnet.messaging.clients.Handlers.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet.messaging.clients
{
    public static class DependencyInjection
    {
        public static void AddClientServices(this IServiceCollection services)
        {
            services.AddHostedService<KafkaConsumerHandler>();
            services.AddSingleton<IMessageProducer, KafkaMessageProducer>();

            services.AddHostedService<RabbitMQConsumerHandler>();
            services.AddSingleton<IMessageProducer, RabbitMQMessageProducer>();
        }
    }
}
