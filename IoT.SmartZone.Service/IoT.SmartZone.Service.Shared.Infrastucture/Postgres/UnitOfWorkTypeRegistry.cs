﻿using IoT.SmartZone.Service.Shared.Infrastucture;
using System;
using System.Collections.Generic;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Postgres;

public class UnitOfWorkTypeRegistry
{
    private readonly Dictionary<string, Type> _types = new();

    public void Register<T>() where T : IUnitOfWork => _types[GetKey<T>()] = typeof(T);

    public Type Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;

    private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";
}