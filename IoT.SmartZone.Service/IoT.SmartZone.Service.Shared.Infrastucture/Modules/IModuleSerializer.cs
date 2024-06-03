using System;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Modules;

public interface IModuleSerializer
{
    byte[] Serialize<T>(T value);
    T Deserialize<T>(byte[] value);
    object Deserialize(byte[] value, Type type);
}