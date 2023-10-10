using Microsoft.AspNetCore.Mvc;
using RadancyBankApplication.Api.DTOs;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Core.Exceptions;

namespace RadancyBankApplication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository, IAccountRepository accountRepository)
    : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserDto user, CancellationToken token)
    {
        try
        {
            await userRepository.CreateUserAsync(user.ToDomainModel(), token);
        }
        catch (UserExistsException uex)
        {
            return BadRequest(uex);
        }

        return Accepted(user);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UserDto user, CancellationToken token)
    {
        try
        {
            await userRepository.UpdateUserAsync(user.ToDomainModel(), token);
        }
        catch (UserNotFoundException unfEx)
        {
            return BadRequest(unfEx);
        }

        return Accepted(user);
    }

    [HttpDelete("delete/{userId:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId, CancellationToken token)
    {
        try
        {
            await userRepository.DeleteUserAsync(userId, token);
        }
        catch (UserNotFoundException unfEx)
        {
            return BadRequest(unfEx);
        }

        return Accepted();
    }

    [HttpGet("/api/users")]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var result = await userRepository.GetUsersAsync(token);
        return Ok(result.Select(x => new UserDto(x)).ToList());
    }

    [HttpGet("{userId:Guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId, CancellationToken token)
    {
        var result = await userRepository.GetUserAsync(userId, token);

        if (result is null)
        {
            return NotFound(userId);
        }
        
        return Ok(new UserDto(result));
    }

    [HttpGet("{userId:Guid}/accounts")]
    public async Task<IActionResult> GetUserAccounts([FromRoute] Guid userId, CancellationToken token)
    {
        var result = await userRepository.GetUserAsync(userId, token);

        if (result is null)
        {
            return NotFound(userId);
        }

        var accounts = await accountRepository.GetUserAccountsAsync(userId, token);

        return Ok(accounts.Select(x => new AccountDto(x)).ToList());
    }
}