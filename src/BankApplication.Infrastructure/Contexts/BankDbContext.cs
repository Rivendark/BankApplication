using Microsoft.EntityFrameworkCore;
using BankApplication.Infrastructure.DBOs;

namespace BankApplication.Infrastructure.Contexts;

public class BankDbContext : DbContext
{
    internal DbSet<UserDbo> Users { get; set; }
    internal DbSet<AccountDbo> Accounts { get; set; }
    internal DbSet<BalanceChangeDbo> BalanceChanges { get; set; }
    internal DbSet<AccountLockDbo> AccountLocks { get; set; }

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

        modelBuilder.Entity<AccountLockDbo>()
            .HasIndex(al => al.AccountId)
            .IsUnique();
    }
}