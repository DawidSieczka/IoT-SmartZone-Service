using System;

namespace IoT.SmartZone.Service.Shared.Abstractions.Time;

public interface IClock
{
    DateTime CurrentDate();
}