using System.Threading;
using System.Threading.Tasks;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Dispatchers;

public interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class, IMessage;
}