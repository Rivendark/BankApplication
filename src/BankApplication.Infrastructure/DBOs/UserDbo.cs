using BankApplication.Core.Models;

namespace BankApplication.Infrastructure.DBOs;

internal sealed class UserDbo
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public List<AccountDbo> Accounts { get; set; } = new ();
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAtUtc { get; set; }
    
    private UserDbo() {}

    public UserDbo(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Accounts = user.Accounts.Select(x => new AccountDbo(x)).ToList();
    }

    public static UserDbo Create(Guid id, string firstName, string lastName, string email)
    {
        return new UserDbo
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }

    public User ToDomainModel()
    {
        return new User
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Accounts = Accounts
                .Where(x => x.DeletedAtUtc != null)
                .Select(x => x.ToDomainModel())
                .ToList()
        };
    }
}