using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Queries.Users;

public sealed class GetUsersQuery : IQuery<List<UserDto>>
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    public List<Guid> UserIds { get; init; } = new ();
}