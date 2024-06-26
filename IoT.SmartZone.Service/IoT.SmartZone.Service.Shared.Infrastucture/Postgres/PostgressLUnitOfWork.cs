﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Postgres;

public abstract class PostgresLUnitOfWork<T> : IUnitOfWork where T : DbContext
{
    private readonly T _dbContext;

    protected PostgresLUnitOfWork(T dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}