using Ardalis.ApiEndpoints;
using BankApplication.Application.DTOs;
using BankApplication.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Api.Endpoints.Users;

[Route(UserEndpoints.ControllerBase)]
public sealed class GetAll(ISender sender) : EndpointBaseAsync
    .WithRequest<GetUsersQuery>
    .WithResult<List<UserDto>>
{
    [HttpGet]
    public override async Task<List<UserDto>> HandleAsync(GetUsersQuery request, CancellationToken cancellationToken = new ())
    {
        return await sender.Send(request, cancellationToken);
    }
}