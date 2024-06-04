using IoT.SmartZone.Service.Shared.Abstractions.Commands;
using IoT.SmartZone.Service.Shared.Abstractions.Events;
using IoT.SmartZone.Service.Shared.Abstractions.Queries;
using IoT.SmartZone.Service.Shared.Infrastucture.MSSQL.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.SmartZone.Service.Shared.Infrastucture.MSSQL;

public static class Extensions
{
    public static Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, IPagedQuery query,
        CancellationToken cancellationToken = default)
        => data.PaginateAsync(query.Page, query.Results, cancellationToken);

    public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int results,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0)
        {
            page = 1;
        }

        results = results switch
        {
            <= 0 => 10,
            > 100 => 100,
            _ => results
        };

        var totalResults = await data.CountAsync();
        var totalPages = totalResults <= results ? 1 : (int)Math.Floor((double)totalResults / results);
        var result = await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);

        return new Paged<T>(result, page, results, totalPages, totalResults);
    }

    public static Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, IPagedQuery query,
        CancellationToken cancellationToken = default)
        => data.SkipAndTakeAsync(query.Page, query.Results, cancellationToken);

    public static async Task<List<T>> SkipAndTakeAsync<T>(this IQueryable<T> data, int page, int results,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0)
        {
            page = 1;
        }

        results = results switch
        {
            <= 0 => 10,
            > 100 => 100,
            _ => results
        };

        return await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);
    }

    public static IServiceCollection AddMSSQL(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("SqlServer");
        services.Configure<MSSQLOptions>(section);
        services.AddSingleton(new UnitOfWorkTypeRegistry());

        return services;
    }

    public static IServiceCollection AddTransactionalDecorators(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));
        services.TryDecorate(typeof(IEventHandler<>), typeof(TransactionalEventHandlerDecorator<>));

        return services;
    }

    public static IServiceCollection AddMSSQL<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        var mssqlOptions = configuration.GetOptions<MSSQLOptions>("SqlServer");
        services.AddDbContext<T>(x => x.UseLazyLoadingProxies()
            .UseSqlServer(mssqlOptions.ConnectionString, options => options.CommandTimeout(120)
            .EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null)));

        return services;
    }

    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
    {
        services.AddScoped<IUnitOfWork, T>();
        services.AddScoped<T>();
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();

        return services;
    }
}