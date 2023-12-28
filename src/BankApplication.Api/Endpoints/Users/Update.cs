using Ardalis.ApiEndpoints;
using BankApplication.Application.Commands.Users;
using BankApplication.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Api.Endpoints.Users;

[Route(UserEndpoints.ControllerBase)]
public class Update(ISender sender) : EndpointBaseAsync
    .WithRequest<UpdateUserCommand>
    .WithResult<UserDto>
{
    [HttpPut]
    public override async Task<UserDto> HandleAsync(UpdateUserCommand request, CancellationToken cancellationToken = new ())
    {
        return await sender.Send(request, cancellationToken);
    }
}