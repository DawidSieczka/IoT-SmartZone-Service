﻿using IoT.SmartZone.Service.Shared.Abstractions.Messaging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Outbox;

public class OutboxTypeRegistry
{
    private readonly Dictionary<string, Type> _types = new();

    public void Register<T>() where T : IOutbox => _types[GetKey<T>()] = typeof(T);

    public Type Resolve(IMessage message) => _types.TryGetValue(GetKey(message.GetType()), out var type) ? type : null;

    private static string GetKey<T>() => GetKey(typeof(T));

    private static string GetKey(Type type)
        => type.IsGenericType
            ? $"{type.GenericTypeArguments[0].GetModuleName()}"
            : $"{type.GetModuleName()}";
}