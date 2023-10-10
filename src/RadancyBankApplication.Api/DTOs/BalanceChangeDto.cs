using RadancyBankApplication.Core.Enums;
using RadancyBankApplication.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace RadancyBankApplication.Api.DTOs;

public class BalanceChangeDto
{
   public Guid Id { get; init; } = Guid.NewGuid();
   [Required]
   public Guid AccountId { get; init; }
   [Required]
   public Guid UserId { get; init; }
   public BalanceChangeType Type { get; set; }
   [Required]
   [Range(0, Int32.MaxValue)]
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

   public BalanceChange ToDomainModel(BalanceChangeType? type = null)
   {
      return new BalanceChange
      {
         Id = Id,
         AccountId = AccountId,
         UserId = UserId,
         Type = type ?? Type,
         Amount = Amount,
         CreatedAtUtc = CreatedAtUtc
      };
   }
}