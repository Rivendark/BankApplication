using RadancyBankApplication.Core.Enums;

namespace RadancyBankApplication.Api.DTOs;

public class BalanceChangeDto
{
   public Guid Id { get; init; }
   public Guid AccountId { get; init; }
   public Guid UserId { get; init; }
   public BalanceChangeType Type { get; init; }
   public decimal Amount { get; init; }
   public DateTime CreatedAtUtc { get; init; }
}