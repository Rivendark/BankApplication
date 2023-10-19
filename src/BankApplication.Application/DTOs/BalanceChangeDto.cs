using BankApplication.Core.Enums;
using BankApplication.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.Application.DTOs;

public sealed class BalanceChangeDto
{
   public Guid Id { get; init; } = Guid.NewGuid();
   public Guid AccountId { get; init; }
   public Guid UserId { get; init; }
   public BalanceChangeType Type { get; init; }
   public decimal Amount { get; init; }
   public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
   
   public BalanceChangeDto() {}

   public BalanceChangeDto(BalanceChange balanceChange)
   {
      Id = balanceChange.Id;
      AccountId = balanceChange.AccountId;
      UserId = balanceChange.UserId;
      Type = balanceChange.Type;
      Amount = balanceChange.Amount;
      CreatedAtUtc = balanceChange.CreatedAtUtc;
   }
}