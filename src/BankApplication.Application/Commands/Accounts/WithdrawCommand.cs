using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Commands.Accounts;

public sealed record WithdrawCommand : ICommand<AccountDto>
{
    public Guid CorrelationId { get; init; }
    public Guid SendingSystemId { get; init; }
    public Guid AccountId { get; init; }
    public Guid UserId { get; init; }
    public decimal Amount { get; init; }
}