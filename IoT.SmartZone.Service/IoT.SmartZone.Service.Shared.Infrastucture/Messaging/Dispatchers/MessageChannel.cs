using System.Threading.Channels;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Dispatchers;

public sealed class MessageChannel : IMessageChannel
{
    private readonly Channel<MessageEnvelope> _messages = Channel.CreateUnbounded<MessageEnvelope>();

    public ChannelReader<MessageEnvelope> Reader => _messages.Reader;
    public ChannelWriter<MessageEnvelope> Writer => _messages.Writer;
}