using RadancyBankApplication.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace RadancyBankApplication.Api.DTOs;

public class UserDto
{
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    public string FirstName { get; init; }
    [Required]
    public string LastName { get; init; }
    public string Email { get; init; }
    
    public UserDto() {}

    public UserDto(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
    }
    
    public User ToDomainModel()
    {
        return new User
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email
        };
    }
}