using BankApplication.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.Application.DTOs;

public sealed class AccountDto
{
    public Guid CorrelationId { get; init; }
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public decimal Balance { get; init; } = 0;
    public DateTime CreatedAtUtc { get; init; }
    public List<BalanceChangeDto> BalanceChanges { get; init; } = new ();
    
    public AccountDto() {}

    public AccountDto(Account account, Guid correlationId)
    {
        CorrelationId = correlationId;
        Id = account.Id;
        UserId = account.UserId;
        Name = account.Name;
        Balance = account.Balance;
        CreatedAtUtc = account.CreatedAtUtc;
        BalanceChanges = account.BalanceChanges.Select(x => new BalanceChangeDto(x)).ToList();
    }
}