using RadancyBankApplication.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace RadancyBankApplication.Api.DTOs;

public class AccountDto
{
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public decimal Balance { get; init; } = 0;
    public DateTime CreatedAtUtc { get; init; }= DateTime.UtcNow;
    public List<BalanceChangeDto> BalanceChanges { get; init; }
    
    public AccountDto() {}

    public AccountDto(Account account)
    {
        Id = account.Id;
        UserId = account.UserId;
        Name = account.Name;
        Balance = account.Balance;
        CreatedAtUtc = account.CreatedAtUtc;
        BalanceChanges = account.BalanceChanges.Select(x => new BalanceChangeDto(x)).ToList();
    }

    public Account ToDomainModel()
    {
        return new Account
        {
            Id = Id,
            UserId = UserId,
            Name = Name,
            Balance = Balance,
            BalanceChanges = BalanceChanges.Select(x => x.ToDomainModel()).ToList()
        };
    }
}