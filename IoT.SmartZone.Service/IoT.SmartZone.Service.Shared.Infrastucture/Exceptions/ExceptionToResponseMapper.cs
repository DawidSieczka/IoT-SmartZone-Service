﻿using Humanizer;
using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Exceptions;

public class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> _codes = new();

    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            ModularException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message))
                , HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new ErrorsResponse(new Error("error", "There was an error.")),
                HttpStatusCode.InternalServerError)
        };

    private record Error(string Code, string Message);

    private record ErrorsResponse(params Error[] Errors);

    private static string GetErrorCode(object exception)
    {
        var type = exception.GetType();
        return _codes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
    }
}