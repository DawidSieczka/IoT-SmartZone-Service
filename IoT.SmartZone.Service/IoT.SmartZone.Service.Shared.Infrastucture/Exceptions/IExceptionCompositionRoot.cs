using System;
using Modular.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}