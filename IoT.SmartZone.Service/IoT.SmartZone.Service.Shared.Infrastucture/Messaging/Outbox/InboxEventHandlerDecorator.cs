using Humanizer;
using IoT.SmartZone.Service.Shared.Abstractions.Events;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Outbox;

[Decorator]
public class InboxEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageContextProvider _messageContextProvider;
    private readonly InboxTypeRegistry _inboxTypeRegistry;
    private readonly bool _enabled;

    public InboxEventHandlerDecorator(IEventHandler<T> handler, IServiceProvider serviceProvider,
        IMessageContextProvider messageContextProvider, InboxTypeRegistry inboxTypeRegistry,
        IOptions<OutboxOptions> options)
    {
        _handler = handler;
        _serviceProvider = serviceProvider;
        _messageContextProvider = messageContextProvider;
        _inboxTypeRegistry = inboxTypeRegistry;
        _enabled = options.Value.Enabled;
    }

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        if (_enabled)
        {
            var inboxType = _inboxTypeRegistry.Resolve<T>();
            if (inboxType is null)
            {
                await _handler.HandleAsync(@event, cancellationToken);
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var inbox = (IInbox)_serviceProvider.GetRequiredService(inboxType);
            var context = _messageContextProvider.Get(@event);
            var name = @event.GetType().Name.Underscore();
            await inbox.HandleAsync(context.MessageId, name, () => _handler.HandleAsync(@event, cancellationToken));
            return;
        }

        await _handler.HandleAsync(@event, cancellationToken);
    }
}