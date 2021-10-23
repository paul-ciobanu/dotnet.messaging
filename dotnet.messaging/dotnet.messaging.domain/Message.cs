namespace dotnet.messaging.domain
{
    public record struct Message(string BrokerType, string Data);
}
