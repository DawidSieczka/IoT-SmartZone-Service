namespace IoT.SmartZone.Service.Shared.Infrastucture.Security.Encryption;

public interface IHasher
{
    string Hash(string data);
}