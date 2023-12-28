using Ardalis.ApiEndpoints;
using BankApplication.Application.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Api.Endpoints.Users;

[Route(UserEndpoints.ControllerBase)]
public class Delete(ISender sender) : EndpointBaseAsync
    .WithRequest<DeleteUserCommand>
    .WithActionResult
{
    public override async Task<ActionResult> HandleAsync(DeleteUserCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        await sender.Send(request, cancellationToken);
        return Accepted();
    }
}