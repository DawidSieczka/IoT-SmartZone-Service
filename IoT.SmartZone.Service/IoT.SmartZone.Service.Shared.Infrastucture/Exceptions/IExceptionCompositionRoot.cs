using System;
using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}