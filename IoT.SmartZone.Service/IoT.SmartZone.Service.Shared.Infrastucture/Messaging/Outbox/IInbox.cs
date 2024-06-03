using System;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Outbox;

public interface IInbox
{
    public bool Enabled { get; }
    Task HandleAsync(Guid messageId, string name, Func<Task> handler);
    Task CleanupAsync(DateTime? to = null);
}