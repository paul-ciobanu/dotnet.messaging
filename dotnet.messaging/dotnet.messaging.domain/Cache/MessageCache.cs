namespace dotnet.messaging.domain.Cache
{
    internal class MessageCache : IMessageCache, IMessageWriteCache
    {
        private readonly List<Message> _messages = new();
        public void Add(Message message)
        {
            _messages.Add(message);
        }

        public IEnumerable<Message> GetAll()
        {
            var result = _messages.ToArray();
            return result;
        }
    }
}
