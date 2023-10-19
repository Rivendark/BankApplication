namespace BankApplication.Application.Services;

public interface IAccountLock
{
    public Task<bool> TryGetLock(Guid id, CancellationToken token);
    public Task ReleaseLock(Guid id, CancellationToken token);
}