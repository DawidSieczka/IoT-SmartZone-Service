using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Shared.Abstractions.Kernel.Exceptions;

public class UnsupportedCurrencyException : ModularException
{
    public string Currency { get; }

    public UnsupportedCurrencyException(string currency) : base($"Currency: '{currency}' is unsupported.")
    {
        Currency = currency;
    }
}