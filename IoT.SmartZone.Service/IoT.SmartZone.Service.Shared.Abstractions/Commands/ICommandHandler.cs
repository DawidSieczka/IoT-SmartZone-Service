﻿using System.Threading;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}