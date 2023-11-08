using BankApplication.Core.Enums;

namespace BankApplication.Core.Models;

public sealed class Account
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string? Name { get; init; }
    public decimal Balance { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime DeletedAtUtc { get; set; }
    public List<BalanceChange> BalanceChanges { private get; init; } = new ();

    public void ApplyBalanceChange(BalanceChange balanceChange)
    {
        switch (balanceChange.Type)
        {
            case BalanceChangeType.Withdrawal:
                Balance -= balanceChange.Amount;
                break;
            case BalanceChangeType.Deposit:
                Balance += balanceChange.Amount;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(balanceChange.Type), balanceChange.Type, "Unknown BalanceChangeType encountered.");
        }
        
        BalanceChanges.Add(balanceChange);
    }

    public List<BalanceChange> GetBalanceChanges()
    {
        return BalanceChanges.ToList();
    }
}