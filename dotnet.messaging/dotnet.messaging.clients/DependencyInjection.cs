using dotnet.messaging.clients.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet.messaging.clients
{
    public static class DependencyInjection
    {
        public static void AddClientServices(this IServiceCollection services)
        {
            services.AddSingleton<KafkaConsumerHandler>();
            services.AddSingleton<KafkaProducerHandler>();
        }
    }
}
