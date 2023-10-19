using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Commands.Users;

public sealed record CreateUserCommand : ICommand<UserDto>
{
    public Guid CorrelationId { get; init; }
    public Guid SendingSystemId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}