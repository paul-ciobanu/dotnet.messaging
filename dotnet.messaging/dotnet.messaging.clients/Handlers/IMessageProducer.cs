namespace dotnet.messaging.clients.Handlers
{
    public interface IMessageProducer
    {
        Task Send(string message);
    }
}
