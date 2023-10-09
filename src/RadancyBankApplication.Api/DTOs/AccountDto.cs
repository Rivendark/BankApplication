namespace RadancyBankApplication.Api.DTOs;

public class AccountDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public decimal Balance { get; init; } = 0;
    public DateTime CreatedAtUtc { get; init; }
    public List<BalanceChangeDto> BalanceChanges { get; init; }
}