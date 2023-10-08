using RadancyBankApplication.Core.Enums;

namespace RadancyBankApplication.Core.Models;

public class BalanceChangeDbo(BalanceChange balanceChange)
{
   public Guid Id { get; set; } = balanceChange.Id;
   public Guid AccountId { get; set; } = balanceChange.AccountId;
   public Guid UserId { get; set; } = balanceChange.UserId;
   public BalanceChangeType Type { get; set; } = balanceChange.Type;
   public decimal Amount { get; set; } = balanceChange.Amount;
   public DateTime CreatedAtUtc { get; set; } = balanceChange.CreatedAtUtc;

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