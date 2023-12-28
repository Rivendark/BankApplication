using Ardalis.ApiEndpoints;
using BankApplication.Application.DTOs;
using BankApplication.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Api.Endpoints.Users;

[Route(UserEndpoints.ControllerBase)]
public sealed class Get(ISender sender) : EndpointBaseAsync
    .WithRequest<GetUserQuery>
    .WithResult<UserDto>
{
    [HttpGet("{userId:guid}")]
    public override async Task<UserDto> HandleAsync(GetUserQuery request, CancellationToken cancellationToken = new ())
    {
        return await sender.Send(request, cancellationToken);
    }
}