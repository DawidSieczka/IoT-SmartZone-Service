﻿using System;
using System.Threading;
using System.Threading.Tasks;
using IoT.SmartZone.Service.Shared.Abstractions.Modules;
using IoT.SmartZone.Service.Shared.Infrastucture.Contexts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Dispatchers;

public sealed class AsyncDispatcherJob : BackgroundService
{
    private readonly IMessageChannel _messageChannel;
    private readonly IModuleClient _moduleClient;
    private readonly ContextAccessor _contextAccessor;
    private readonly ILogger<AsyncDispatcherJob> _logger;

    public AsyncDispatcherJob(IMessageChannel messageChannel, IModuleClient moduleClient,
        ContextAccessor contextAccessor, ILogger<AsyncDispatcherJob> logger)
    {
        _messageChannel = messageChannel;
        _moduleClient = moduleClient;
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Running the async dispatcher.");
        await foreach (var envelope in _messageChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                _contextAccessor.Context ??= envelope.MessageContext.Context;
                await _moduleClient.PublishAsync(envelope.Message, stoppingToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }

        _logger.LogInformation("Finished running the async dispatcher.");
    }
}