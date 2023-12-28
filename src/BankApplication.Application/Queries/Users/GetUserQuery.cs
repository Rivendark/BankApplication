using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Application.Queries.Users;

public sealed record GetUserQuery : IQuery<UserDto>
{
    [FromQuery(Name = "correlationId")]
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    
    [FromRoute(Name = "userId")]
    public Guid UserId { get; init; }
}