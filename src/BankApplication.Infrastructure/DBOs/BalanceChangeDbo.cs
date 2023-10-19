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
   
   public BalanceChangeDbo() {}

   public BalanceChangeDbo(BalanceChange balanceChange)
   {
      Id = balanceChange.Id;
      AccountId = balanceChange.AccountId;
      UserId = balanceChange.UserId;
      Type = balanceChange.Type;
      Amount = balanceChange.Amount;
      CreatedAtUtc = balanceChange.CreatedAtUtc;
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