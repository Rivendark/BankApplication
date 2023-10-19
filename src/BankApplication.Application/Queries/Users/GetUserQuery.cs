using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Queries.Users;

public sealed class GetUserQuery : IQuery<UserDto>
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    public Guid UserId { get; init; }
}