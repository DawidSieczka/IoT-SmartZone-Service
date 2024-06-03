namespace IoT.SmartZone.Service.Shared.Infrastucture.Security;

public sealed class SecurityOptions
{
    public EncryptionOptions Encryption { get; set; }

    public class EncryptionOptions
    {
        public bool Enabled { get; set; }
        public string Key { get; set; }
    }
}