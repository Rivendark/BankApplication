using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Commands.Users;

public sealed record DeleteUserCommand : ICommand
{
    public Guid CorrelationId { get; init; }
    public Guid SendingSystemId { get; init; }
    public Guid UserId { get; init; }
}