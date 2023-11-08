using BankApplication.Core.Enums;
using BankApplication.Core.Models;

namespace BankApplication.Infrastructure.DBOs;

internal sealed class BalanceChangeDbo
{
   public Guid Id { get; set; }
   public Guid AccountId { get; set; }
   public Guid UserId { get; set; }
   public BalanceChangeType Type { get; set; }
   public decimal Amount { get; set; }
   public DateTime CreatedAtUtc { get; set; }
   
   private BalanceChangeDbo() {}

   public BalanceChangeDbo(BalanceChange balanceChange)
   {
      Id = balanceChange.Id;
      AccountId = balanceChange.AccountId;
      UserId = balanceChange.UserId;
      Type = balanceChange.Type;
      Amount = balanceChange.Amount;
      CreatedAtUtc = balanceChange.CreatedAtUtc;
   }

   public static BalanceChangeDbo Create(
      Guid id,
      Guid accountId,
      Guid userId,
      BalanceChangeType type,
      decimal amount = 0,
      DateTime? createdAtUtc = null)
   {
      return new BalanceChangeDbo
      {
         Id = id,
         AccountId = accountId,
         UserId = userId,
         Type = type,
         Amount = amount,
         CreatedAtUtc = createdAtUtc ?? DateTime.UtcNow
      };
   }

   public BalanceChange ToDomainModel()
   {
      return new BalanceChange
      {
         Id = Id,
         AccountId = AccountId,
         UserId = UserId,
         Type = Type,
         Amount = Amount,
         CreatedAtUtc = CreatedAtUtc
      };
   }
}