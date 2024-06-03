using IoT.SmartZone.Service.Shared.Abstractions.Time;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}