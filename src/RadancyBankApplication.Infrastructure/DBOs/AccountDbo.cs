using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Infrastructure.DBOs;

public class AccountDbo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; } = 0;
    public DateTime CreatedAtUtc { get; set; }
    public List<BalanceChangeDbo> BalanceChanges { get; set; }

    public AccountDbo() { }

    public AccountDbo(Account account)
    {
        Id = account.Id;
        UserId = account.UserId;
        Name = account.Name;
        Balance = account.Balance;
        BalanceChanges = account.BalanceChanges.Select(x => new BalanceChangeDbo(x)).ToList();
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