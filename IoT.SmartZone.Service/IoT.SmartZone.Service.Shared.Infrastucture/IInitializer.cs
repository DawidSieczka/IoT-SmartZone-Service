using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Shared.Infrastucture;

public interface IInitializer
{
    Task InitAsync();
}