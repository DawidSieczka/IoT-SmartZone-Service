namespace IoT.SmartZone.Service.Shared.Infrastucture.Security.Encryption;

public interface IRng
{
    string Generate(int length = 50, bool removeSpecialChars = true);
}