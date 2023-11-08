using BankApplication.Core.Models;

namespace BankApplication.Infrastructure.DBOs;

public sealed class AccountLockDbo
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid ProcessorId { get; set; }
    public DateTime LockTime { get; set; }
    
    private AccountLockDbo() {}

    public AccountLockDbo(AccountLock accountLock)
    {
        Id = accountLock.Id;
        AccountId = accountLock.AccountId;
        ProcessorId = accountLock.ProcessorId;
        LockTime = accountLock.LockTime;
    }

    public AccountLock ToDomainModel()
    {
        return new AccountLock
        {
            Id = Id,
            AccountId = AccountId,
            ProcessorId = ProcessorId,
            LockTime = LockTime
        };
    }
}