using System.Collections.Generic;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Api;

public sealed class CorsOptions
{
    public bool AllowCredentials { get; set; }
    public IEnumerable<string> AllowedOrigins { get; set; }
    public IEnumerable<string> AllowedMethods { get; set; }
    public IEnumerable<string> AllowedHeaders { get; set; }
    public IEnumerable<string> ExposedHeaders { get; set; }
}