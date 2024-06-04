using System;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Shared.Infrastucture.MSSQL;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}