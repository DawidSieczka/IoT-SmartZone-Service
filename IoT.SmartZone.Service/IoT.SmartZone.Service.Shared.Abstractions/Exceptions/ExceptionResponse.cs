using System.Net;

namespace IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);