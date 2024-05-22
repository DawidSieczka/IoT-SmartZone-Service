using Microsoft.OpenApi.Models;
using System.Reflection;

namespace IoT.SmartZone.Service.Api.Extensions;

public static class SwaggerExtensions
{
    public static void ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "IoT SmartZone Service Api API",
                Description = "Smart Zone - User friendly data representation as API service.",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}