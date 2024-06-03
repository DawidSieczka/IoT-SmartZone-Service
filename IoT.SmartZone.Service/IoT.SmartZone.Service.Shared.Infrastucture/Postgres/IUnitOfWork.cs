using System;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Postgres;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}