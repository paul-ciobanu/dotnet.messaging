using System.Text.Json;
using Confluent.Kafka;
using dotnet.messaging.domain;

namespace dotnet.messaging.clients.Handlers
{
    public class MessageSerializer : ISerializer<Message>, IDeserializer<Message>
    {
        public Message Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<Message>(data);
        }

        public byte[] Serialize(Message data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, typeof(Message));
        }
    }
}
