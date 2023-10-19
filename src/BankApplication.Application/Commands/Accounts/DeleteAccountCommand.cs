using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Commands.Accounts;

public sealed record DeleteAccountCommand : ICommand
{
    public Guid CorrelationId { get; init; }
    public Guid SendingSystemId { get; init; }
    public Guid AccountId { get; set; }
}