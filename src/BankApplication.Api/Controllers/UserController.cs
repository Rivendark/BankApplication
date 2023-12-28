using MediatR;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Application.Commands.Users;
using BankApplication.Application.Queries.Users;
using BankApplication.Core.Exceptions;

namespace BankApplication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ISender sender)
    : ControllerBase
{
    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken token)
    // {
    //     var result = await sender.Send(command, token);
    //     return Accepted(result);
    // }

    // [HttpPut]
    // public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, CancellationToken token)
    // {
    //     return Accepted(await sender.Send(command, token));
    // }

    // [HttpDelete]
    // public async Task<IActionResult> Delete([FromBody] DeleteUserCommand command, CancellationToken token)
    // {
    //     await sender.Send(command, token);
    //     return Accepted();
    // }

    // [HttpGet("/api/users")]
    // public async Task<IActionResult> GetUsers(CancellationToken token)
    // {
    //     return Ok(await sender.Send(new GetUsersQuery(), token));
    // }
    //
    // [HttpPost("/api/users")]
    // public async Task<IActionResult> GetSelectUsers([FromBody] GetUsersQuery query, CancellationToken token)
    // {
    //     return Ok(await sender.Send(query, token));
    // }
    //
    // [HttpGet("{userId:Guid}")]
    // public async Task<IActionResult> GetUser(
    //     [FromRoute] Guid userId,
    //     CancellationToken token,
    //     [FromQuery] Guid? correlationId = null)
    // {
    //     var result = await sender.Send(new GetUserQuery
    //     {
    //         UserId = userId,
    //         CorrelationId = correlationId ?? Guid.NewGuid()
    //     }, token);
    //
    //     return Ok(result);
    // }
}