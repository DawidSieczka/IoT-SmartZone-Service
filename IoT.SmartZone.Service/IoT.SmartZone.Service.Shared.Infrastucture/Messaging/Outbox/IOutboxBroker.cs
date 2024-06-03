using System.Threading.Tasks;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Outbox;

public interface IOutboxBroker
{
    bool Enabled { get; }
    Task SendAsync(params IMessage[] messages);
}