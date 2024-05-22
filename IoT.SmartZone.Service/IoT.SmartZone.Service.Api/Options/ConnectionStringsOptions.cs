namespace IoT.SmartZone.Service.Api.Options;

public class ConnectionStringsOptions
{
    public const string ConnectionStrings = "ConnectionStrings";

    public string SqlDatabase { get; set; } = string.Empty;
}