namespace IoT.SmartZone.Service.Shared.Infrastucture.Security.Encryption;

public interface IMd5
{
    string Calculate(string value);
}