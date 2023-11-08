namespace BankApplication.Core.Models;

public sealed class AccountLock
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid AccountId { get; init; }
    public Guid ProcessorId { get; init; }
    public DateTime LockTime { get; init; } = DateTime.UtcNow;
    
    public AccountLock() {}
}