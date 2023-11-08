using BankApplication.Core.Models;

namespace BankApplication.Infrastructure.DBOs;

internal sealed class AccountDbo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? DeletedAtUtc { get; set; }
    public List<BalanceChangeDbo> BalanceChanges { get; set; } = new ();

    private AccountDbo() { }

    public AccountDbo(Account account)
    {
        Id = account.Id;
        UserId = account.UserId;
        Name = account.Name ?? string.Empty;
        Balance = account.Balance;
        CreatedAtUtc = account.CreatedAtUtc;

        if (account.GetBalanceChanges().Count != 0)
        {
            BalanceChanges = account.GetBalanceChanges()
                .Select(x => new BalanceChangeDbo(x))
                .ToList();
        }
    }

    public static AccountDbo Create(
        Guid id,
        Guid userId,
        string name,
        decimal balance = 0,
        DateTime? createdAtUtc = null)
    {
        return new AccountDbo
        {
            Id = id,
            UserId = userId,
            Name = name,
            Balance = balance,
            CreatedAtUtc = createdAtUtc ?? DateTime.UtcNow
        };
    }

    public Account ToDomainModel()
    {
        return new Account
        {
            Id = Id,
            UserId = UserId,
            Name = Name,
            Balance = Balance,
            CreatedAtUtc = CreatedAtUtc,
            BalanceChanges = BalanceChanges.Select(x => x.ToDomainModel()).ToList()
        };
    }
}