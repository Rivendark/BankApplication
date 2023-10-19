using System.Collections.Concurrent;

namespace BankApplication.Application.Services;

public class AccountLock() : IAccountLock
{
    private readonly ConcurrentDictionary<Guid, DateTime> _locks = new();
    
    public async Task<bool> TryGetLock(Guid id, CancellationToken token)
    {
        var tries = 0;
        while (tries < 3)
        {
            if (HasValidExistingLock(id))
            {
                await Task.Delay(50, token);
                tries++;
                continue;
            }
            
            _locks[id] = DateTime.UtcNow;
            return true;
        }
        return false;
    }

    public async Task ReleaseLock(Guid id, CancellationToken token)
    {
        if (_locks.ContainsKey(id))
        {
            _locks.Remove(id, out _);
            await Task.Delay(1, token);
        }
    }

    private bool HasValidExistingLock(Guid id)
    {
        if (!_locks.ContainsKey(id))
        {
            return false;
        }

        if (_locks[id] >= DateTime.UtcNow.AddMicroseconds(-500)) return true;
        
        _locks.Remove(id, out _);
        return false;

    }
}