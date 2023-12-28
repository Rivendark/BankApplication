using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Application.Queries.Users;

public sealed record GetUsersQuery : IQuery<List<UserDto>>
{
    [FromQuery(Name = "correlationId")]
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    
    [FromBody]
    public List<Guid> UserIds { get; init; } = new ();
}