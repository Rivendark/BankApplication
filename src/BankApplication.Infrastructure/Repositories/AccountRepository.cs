using Microsoft.EntityFrameworkCore;
using BankApplication.Application.Repositories;
using BankApplication.Core.Enums;
using BankApplication.Core.Exceptions;
using BankApplication.Core.Models;
using BankApplication.Infrastructure.Contexts;
using BankApplication.Infrastructure.DBOs;

namespace BankApplication.Infrastructure.Repositories;

public sealed class AccountRepository(BankDbContext context) : IAccountRepository
{
    public async Task<Account> CreateAccountAsync(Account account, CancellationToken token)
    {
        var existingAccount = await FindAccountAsync(account.Id, token);
        if (existingAccount is not null)
        {
            throw new AccountExistsException();
        }

        account.Balance = 0;
        account.CreatedAtUtc = DateTime.UtcNow;

        var dbo = new AccountDbo(account);
        await context.Accounts.AddAsync(dbo, token);
        await context.SaveChangesAsync(token);

        return dbo.ToDomainModel();
    }

    public async Task<List<Account>> GetUserAccountsAsync(Guid userId, CancellationToken token)
    {
        var accounts = await context.Accounts.Where(x => x.UserId == userId)
            .ToListAsync(token);

        return accounts.Count == 0 ? new List<Account>()
            : accounts.Select(x => x.ToDomainModel()).ToList();
    }

    public async Task<Account?> GetAccountAsync(Guid id, CancellationToken token)
    {
        var result = await FindAccountAsync(id, token);

        return result?.ToDomainModel();
    }

    public async Task<Account> UpdateAccountInformationAsync(Account account, CancellationToken token)
    {
        var result = await FindAccountAsync(account.Id, token);

        if (result is null)
        {
            throw new AccountNotFoundException();
        }

        result.Name = account.Name;

        context.Accounts.Update(result);
        await context.SaveChangesAsync(token);

        return result.ToDomainModel();
    }

    public async Task<Account> UpdateAccountBalanceAsync(BalanceChange balanceChange, CancellationToken token)
    {
        var result = await FindAccountAsync(balanceChange.AccountId, token);

        if (result is null)
        {
            throw new AccountNotFoundException();
        }

        switch (balanceChange.Type)
        {
            case BalanceChangeType.Withdrawal:
                result.Balance -= balanceChange.Amount;
                break;
            case BalanceChangeType.Deposit:
                result.Balance += balanceChange.Amount;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(balanceChange.Type), balanceChange.Type, "Unknown BalanceChangeType encountered.");
        }
        
        context.Accounts.Update(result);
        await context.SaveChangesAsync(token);

        return result.ToDomainModel();
    }

    public async Task DeleteAccountAsync(Guid id, CancellationToken token)
    {
        var result = await FindAccountAsync(id, token);
        if (result is null)
        {
            throw new AccountNotFoundException();
        }

        context.Accounts.Remove(result);
        await context.SaveChangesAsync(token);
        
        
    }

    public async Task SaveBalanceChangeAsync(BalanceChange balanceChange, CancellationToken token)
    {
        await context.BalanceChanges.AddAsync(new BalanceChangeDbo(balanceChange), token);
        await context.SaveChangesAsync(token);
    }

    public async Task<List<BalanceChange>> GetBalanceChangesAsync(Guid accountId, CancellationToken token)
    {
        var results = await context.BalanceChanges.Where(x => x.AccountId == accountId)
            .ToListAsync(token);

        if (results.Count == 0)
        {
            return new List<BalanceChange>();
        }

        return results.Select(x => x.ToDomainModel()).ToList();
    }

    private async Task<AccountDbo?> FindAccountAsync(Guid id, CancellationToken token)
    {
        return await context.Accounts.FindAsync(new object?[] { id }, cancellationToken: token);
    }
}