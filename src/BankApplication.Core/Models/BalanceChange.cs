using BankApplication.Core.Enums;

namespace BankApplication.Core.Models;

public class BalanceChange
{
   public Guid Id { get; set; }
   public Guid AccountId { get; set; }
   public Guid UserId { get; set; }
   public BalanceChangeType Type { get; set; }
   public decimal Amount { get; set; }
   public DateTime CreatedAtUtc { get; set; }
}