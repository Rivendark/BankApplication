using BankApplication.Application.Commands.Accounts;
using BankApplication.Application.Queries.Accounts;
using BankApplication.Core.Exceptions;
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
        try
        {
            return Accepted(await sender.Send(cmd, token));
        }
        catch (AccountExistsException aeEx)
        {
            return BadRequest(aeEx.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAccountCommand cmd, CancellationToken token)
    {
        try
        {
            return Accepted(await sender.Send(cmd, token));
        }
        catch (AccountNotFoundException anfEx)
        {
            return BadRequest(anfEx.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteAccountCommand cmd, CancellationToken token)
    {
        try
        {
            await sender.Send(cmd, token);
            return Accepted();
        }
        catch (AccountNotFoundException anfEx)
        {
            return BadRequest(anfEx.Message);
        }
    }

    [HttpGet("{accountId:Guid}")]
    public async Task<IActionResult> GetAccount(
        [FromRoute] Guid accountId,
        CancellationToken token,
        [FromQuery] Guid? correlationId = null)
    {
        try
        {
            var result = await sender.Send(new GetAccountQuery
            {
                CorrelationId = correlationId ?? Guid.NewGuid(),
                AccountId = accountId
            }, token);

            return Ok(result);
        }
        catch (AccountNotFoundException anfEx)
        {
            return NotFound(anfEx);
        }
    }
    
    [HttpGet("/api/accounts/{userId:Guid}")]
    public async Task<IActionResult> GetAccountsByUserId(
        [FromRoute] Guid userId,
        CancellationToken token,
        [FromQuery] Guid? correlationId = null)
    {
        try
        {
            var accounts = await sender.Send(new GetAccountsByUserIdQuery
            {
                CorrelationId = correlationId ?? Guid.NewGuid(),
                UserId = userId
            }, token);

            return Ok(accounts);
        }
        catch (UserNotFoundException unfEx)
        {
            return BadRequest(unfEx);
        }
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand cmd, CancellationToken token)
    {
        try
        {
            return Accepted(await sender.Send(cmd, token));
        }
        catch (AccountNotFoundException anfEx)
        {
            return BadRequest(anfEx.Message);
        }
        catch (InsufficientAccountBalanceException afnEx)
        {
            return BadRequest(afnEx.Message);
        }
        catch (WithdrawalPercentageExceededException wpeEx)
        {
            return BadRequest(wpeEx.Message);
        }
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositCommand cmd, CancellationToken token)
    {
        try
        {
            return Accepted(await sender.Send(cmd, token));
        }
        catch (DepositLimitExceededException dleEx)
        {
            return BadRequest(dleEx.Message);
        }
    }
}