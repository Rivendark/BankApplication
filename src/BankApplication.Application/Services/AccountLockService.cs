using BankApplication.Application.Repositories;
using BankApplication.Core.Models;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Services;

public class AccountLockService(IAccountLockRepository repository, ILogger<AccountLockService> logger) : IAccountLockService
{
    private readonly Guid _processorId = Guid.NewGuid();
    private AccountLock? _accountLock;
    
    public async Task<bool> TryGetLockAsync(Guid id, CancellationToken token)
    {
        var accountLock = await repository.TryGetAccountLockAsync(_processorId, id, token);
        _accountLock = accountLock;
        logger.LogInformation($"Lock obtained with id: {_accountLock?.Id}");
        return accountLock is not null;
    }

    public async Task ReleaseLockAsync(CancellationToken token)
    {
        if (_accountLock is not null)
        {
            await repository.ReleaseAccountLockAsync(_accountLock, token);
            logger.LogInformation($"Lock released with id: {_accountLock.Id}");
        }
    }
}