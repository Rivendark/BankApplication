using Microsoft.AspNetCore.Mvc;
using RadancyBankApplication.Api.DTOs;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Core.Exceptions;

namespace RadancyBankApplication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserDto user, CancellationToken token)
    {
        try
        {
            await _userRepository.CreateUserAsync(user.ToDomainModel(), token);
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
            await _userRepository.UpdateUserAsync(user.ToDomainModel(), token);
        }
        catch (UserNotFoundException unfEx)
        {
            return BadRequest(unfEx);
        }

        return Accepted(user);
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken token)
    {
        try
        {
            await _userRepository.DeleteUserAsync(id, token);
        }
        catch (UserNotFoundException unfEx)
        {
            return BadRequest(unfEx);
        }

        return Accepted(id);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var result = await _userRepository.GetUsersAsync(token);
        return Ok(result.Select(x => new UserDto(x)).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser([FromQuery] Guid id, CancellationToken token)
    {
        var result = await _userRepository.GetUserAsync(id, token);

        if (result is null)
        {
            return NotFound(id);
        }
        
        return Ok(new UserDto(result));
    }
}