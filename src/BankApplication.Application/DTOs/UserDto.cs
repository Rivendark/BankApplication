using BankApplication.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.Application.DTOs;

public sealed class UserDto
{
    public Guid CorrelationId { get; init; }
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; init; }
    [Required]
    public string LastName { get; init; }
    public string Email { get; init; }
    
    public UserDto() {}

    public UserDto(User user, Guid correlationId = default)
    {
        CorrelationId = correlationId;
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
    }
}