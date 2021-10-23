using dotnet.messaging.domain.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet.messaging.domain
{
    public static class DependencyInjection
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<MessageCache>();
            services.AddSingleton<IMessageWriteCache, MessageCache>();

            services.AddSingleton<IMessageCache>(x => x.GetRequiredService<MessageCache>());
            services.AddSingleton<IMessageWriteCache>(x => x.GetRequiredService<MessageCache>());
        }
    }
}
