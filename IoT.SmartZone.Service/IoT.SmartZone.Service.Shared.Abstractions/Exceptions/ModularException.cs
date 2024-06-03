using System;

namespace IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

public abstract class ModularException : Exception
{
    protected ModularException(string message) : base(message)
    {
    }
}