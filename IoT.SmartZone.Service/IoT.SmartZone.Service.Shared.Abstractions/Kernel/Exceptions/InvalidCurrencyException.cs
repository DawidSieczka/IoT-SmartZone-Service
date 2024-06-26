using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Shared.Abstractions.Kernel.Exceptions;

public class InvalidCurrencyException : ModularException
{
    public string Currency { get; }

    public InvalidCurrencyException(string currency) : base($"Currency: '{currency}' is invalid.")
    {
        Currency = currency;
    }
}