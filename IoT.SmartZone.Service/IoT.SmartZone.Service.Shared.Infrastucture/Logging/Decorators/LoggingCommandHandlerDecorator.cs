using Humanizer;
using IoT.SmartZone.Service.Shared.Abstractions.Commands;
using IoT.SmartZone.Service.Shared.Abstractions.Contexts;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Logging.Decorators;

[Decorator]
public sealed class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IMessageContextProvider _messageContextProvider;
    private readonly IContext _context;
    private readonly ILogger<LoggingCommandHandlerDecorator<T>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<T> handler, IMessageContextProvider messageContextProvider,
        IContext context, ILogger<LoggingCommandHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _messageContextProvider = messageContextProvider;
        _context = context;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();
        var messageContext = _messageContextProvider.Get(command);
        var requestId = _context.RequestId;
        var traceId = _context.TraceId;
        var userId = _context.Identity?.Id;
        var messageId = messageContext?.MessageId;
        var correlationId = messageContext?.Context.CorrelationId ?? _context.CorrelationId;
        _logger.LogInformation("Handling a command: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]'...",
            name, module, requestId, messageId, correlationId, traceId, userId);
        await _handler.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Handled a command: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}']",
            name, module, requestId, messageId, correlationId, traceId, userId);
    }
}