
using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Shared.Abstractions.Kernel.Exceptions;

public class InvalidAmountException : ModularException
{
    public decimal Amount { get; }

    public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
    {
        Amount = amount;
    }
}