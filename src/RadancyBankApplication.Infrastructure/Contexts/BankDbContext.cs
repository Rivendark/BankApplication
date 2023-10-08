using Microsoft.EntityFrameworkCore;
using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Infrastructure.Contexts;

public class BankDbContext : DbContext
{
    public DbSet<UserDbo> Users { get; set; }
    public DbSet<AccountDbo> Accounts { get; set; }
    public DbSet<BalanceChangeDbo> BalanceChanges { get; set; }

    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDbo>()
            .HasMany(e => e.Accounts)
            .WithOne()
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<AccountDbo>()
            .HasMany(e => e.BalanceChanges)
            .WithOne()
            .HasForeignKey(e => e.AccountId);
    }
}