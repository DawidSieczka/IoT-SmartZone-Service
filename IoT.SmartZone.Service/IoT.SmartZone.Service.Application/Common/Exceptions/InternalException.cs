namespace IoT.SmartZone.Service.Application.Common.Exceptions;

public class InternalException : CustomException
{
    public override int StatusCode { get; set; } = 500;

    public InternalException(string message) : base(message)
    {
    }

    public InternalException() : base($"Internal action exception. Please contact the support.")
    {
    }
}