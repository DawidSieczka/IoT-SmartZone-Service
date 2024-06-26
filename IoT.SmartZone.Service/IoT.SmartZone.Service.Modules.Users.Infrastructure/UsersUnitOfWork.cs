using IoT.SmartZone.Service.Shared.Infrastucture.Postgres;

namespace IoT.SmartZone.Service.Modules.Users.Infrastructure;

internal class UsersUnitOfWork : PostgresLUnitOfWork<UsersDbContext>
{
    public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
    }
}