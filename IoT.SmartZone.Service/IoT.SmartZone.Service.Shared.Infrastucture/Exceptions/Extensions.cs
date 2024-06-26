﻿using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Exceptions;

public static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>()
            .AddSingleton<IExceptionCompositionRoot, ExceptionCompositionRoot>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();
}