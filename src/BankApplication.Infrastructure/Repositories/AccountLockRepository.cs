using BankApplication.Application.Repositories;
using BankApplication.Core.Models;
using BankApplication.Infrastructure.Contexts;
using BankApplication.Infrastructure.DBOs;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Infrastructure.Repositories;

public sealed class AccountLockRepository(BankDbContext context) : IAccountLockRepository
{
    public async Task<AccountLock?> TryGetAccountLockAsync(Guid processorId, Guid accountId, CancellationToken token)
    {
        var retry = 0;
        while (retry < 3)
        {
            try
            {
                var expiredLocks = await context.AccountLocks.Where(x =>
                    x.AccountId == accountId
                    && x.LockTime <= DateTime.UtcNow.AddSeconds(-30))
                    .ToListAsync(token);

                context.AccountLocks.RemoveRange(expiredLocks);
                await context.SaveChangesAsync(token);

                var dbo = new AccountLockDbo(new AccountLock
                {
                    AccountId = accountId,
                    ProcessorId = processorId
                });
                await context.AccountLocks.AddAsync(dbo, token);
                await context.SaveChangesAsync(token);

                var accountLock = await context.AccountLocks
                    .Where(x => x.AccountId == accountId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(token);

                if (accountLock != null && accountLock.ProcessorId == processorId)
                    return accountLock.ToDomainModel();
                
                await Task.Delay(250, token);
                retry++;
            }
            catch (UniqueConstraintException)
            {
                await Task.Delay(250, token);
                retry++;
            }
        }

        return null;
    }

    public async Task ReleaseAccountLockAsync(AccountLock accountLock, CancellationToken token)
    {
        var dbo = await context.AccountLocks.FindAsync(new object?[] { accountLock.Id }, cancellationToken: token);
        if (dbo is not null)
        {
            context.AccountLocks.Remove(dbo);
            await context.SaveChangesAsync(token);
        }
    }
}