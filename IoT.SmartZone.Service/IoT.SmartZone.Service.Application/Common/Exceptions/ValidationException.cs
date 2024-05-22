namespace IoT.SmartZone.Service.Application.Common.Exceptions;

public class ValidationException : CustomException
{
    public override int StatusCode { get; set; } = 400;

    public ValidationException(string message) : base(message)
    {
    }
}