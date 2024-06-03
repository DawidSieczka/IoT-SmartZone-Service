using System.Threading.Channels;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Dispatchers;

public interface IMessageChannel
{
    ChannelReader<MessageEnvelope> Reader { get; }
    ChannelWriter<MessageEnvelope> Writer { get; }
}