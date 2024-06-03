using System;
using IoT.SmartZone.Service.Shared.Infrastucture.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Security;

public static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("security");
        var securityOptions = section.GetOptions<SecurityOptions>();
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var logger = serviceProvider.GetRequiredService<ILogger<ISecurityProvider>>();
            logger.LogInformation(securityOptions.Encryption.Enabled
                ? "AES-256 data encryption is enabled."
                : "Data encryption is disabled.");
        }

        if (securityOptions.Encryption.Enabled)
        {
            if (string.IsNullOrWhiteSpace(securityOptions.Encryption.Key))
            {
                throw new ArgumentException("Empty encryption key.", nameof(securityOptions.Encryption.Key));
            }

            var keyLength = securityOptions.Encryption.Key.Length;
            if (keyLength != 32)
            {
                throw new ArgumentException($"Invalid encryption key length: {keyLength} (required: 32 chars).",
                    nameof(securityOptions.Encryption.Key));
            }
        }

        return services
            .Configure<SecurityOptions>(section)
            .AddSingleton<ISecurityProvider, SecurityProvider>()
            .AddSingleton<IEncryptor, Encryptor>()
            .AddSingleton<IHasher, Hasher>()
            .AddSingleton<IMd5, Md5>()
            .AddSingleton<IRng, Rng>();
    }
}