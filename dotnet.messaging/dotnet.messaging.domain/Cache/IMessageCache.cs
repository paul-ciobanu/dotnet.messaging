namespace dotnet.messaging.domain.Cache
{
    public interface IMessageCache
    {
        IEnumerable<Message> GetAll();
    }
}
