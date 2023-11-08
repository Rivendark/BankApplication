namespace BankApplication.Application.Services;

public interface IAccountLockService
{
    public Task<bool> TryGetLockAsync(Guid id, CancellationToken token);
    public Task ReleaseLockAsync(CancellationToken token);
}