using BankApplication.Application.Commands.Accounts;
using BankApplication.Application.Queries.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand cmd, CancellationToken token)
    {
        return Accepted(await sender.Send(cmd, token));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAccountCommand cmd, CancellationToken token)
    {
        return Accepted(await sender.Send(cmd, token));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteAccountCommand cmd, CancellationToken token)
    {
        await sender.Send(cmd, token);
        return Accepted();
    }

    [HttpGet("{accountId:Guid}")]
    public async Task<IActionResult> GetAccount(
        [FromRoute] Guid accountId,
        CancellationToken token,
        [FromQuery] Guid? correlationId = null)
    {
        var result = await sender.Send(new GetAccountQuery
        {
            CorrelationId = correlationId ?? Guid.NewGuid(),
            AccountId = accountId
        }, token);

        return Ok(result);
    }
    
    [HttpGet("/api/accounts/{userId:Guid}")]
    public async Task<IActionResult> GetAccountsByUserId(
        [FromRoute] Guid userId,
        CancellationToken token,
        [FromQuery] Guid? correlationId = null)
    {
        var accounts = await sender.Send(new GetAccountsByUserIdQuery
        {
            CorrelationId = correlationId ?? Guid.NewGuid(),
            UserId = userId
        }, token);

        return Ok(accounts);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand cmd, CancellationToken token)
    {
        return Accepted(await sender.Send(cmd, token));
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositCommand cmd, CancellationToken token)
    {
        return Accepted(await sender.Send(cmd, token));
    }
}