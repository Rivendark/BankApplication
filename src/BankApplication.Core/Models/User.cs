namespace BankApplication.Core.Models;

public sealed class User
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Email { get; init; }
    public List<Account> Accounts { get; init; } = new ();
}