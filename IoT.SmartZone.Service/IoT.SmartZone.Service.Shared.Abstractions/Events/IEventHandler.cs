﻿using System.Threading;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Shared.Abstractions.Events;

public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}