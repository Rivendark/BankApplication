namespace BankApplication.Core.Models;

public class Account
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; } = 0;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime DeletedAtUtc { get; set; }
    public List<BalanceChange> BalanceChanges { get; set; } = new ();
}