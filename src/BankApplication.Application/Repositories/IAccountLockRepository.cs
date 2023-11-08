using BankApplication.Core.Models;

namespace BankApplication.Application.Repositories;

public interface IAccountLockRepository
{
    public Task<AccountLock?> TryGetAccountLockAsync(Guid processorId, Guid accountId, CancellationToken token);
    public Task ReleaseAccountLockAsync(AccountLock accountLock, CancellationToken token);
}