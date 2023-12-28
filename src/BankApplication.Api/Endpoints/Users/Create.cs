using Ardalis.ApiEndpoints;
using BankApplication.Application.Commands.Users;
using BankApplication.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Api.Endpoints.Users;

[Route(UserEndpoints.ControllerBase)]
public sealed class Create(ISender sender) : EndpointBaseAsync
    .WithRequest<CreateUserCommand>
    .WithResult<UserDto>
{
    [HttpPost]
    public override async Task<UserDto> HandleAsync([FromBody] CreateUserCommand request, CancellationToken cancellationToken = new ())
    {
        return await sender.Send(request, cancellationToken);
    }
}