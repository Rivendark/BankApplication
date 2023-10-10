using Microsoft.EntityFrameworkCore;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Core.Enums;
using RadancyBankApplication.Core.Exceptions;
using RadancyBankApplication.Core.Models;
using RadancyBankApplication.Infrastructure.Contexts;
using RadancyBankApplication.Infrastructure.DBOs;

namespace RadancyBankApplication.Infrastructure.Repositories;

public class AccountRepository(BankDbContext context) : IAccountRepository
{
    public async Task CreateAccountAsync(Account account, CancellationToken token)
    {
        var existingAccount = await FindAccountAsync(account.Id, token);
        if (existingAccount is not null)
        {
            throw new AccountExistsException();
        }

        account.Balance = 0;
        account.CreatedAtUtc = DateTime.UtcNow;
        
        await context.Accounts.AddAsync(new AccountDbo(account), token);
        await context.SaveChangesAsync(token);
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

    public async Task UpdateAccountInformationAsync(Account account, CancellationToken token)
    {
        var result = await FindAccountAsync(account.Id, token);

        if (result is null)
        {
            throw new AccountNotFoundException();
        }

        result.Name = account.Name;

        context.Accounts.Update(result);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAccountBalanceAsync(Guid accountId, decimal value, BalanceChangeType type, CancellationToken token)
    {
        var result = await FindAccountAsync(accountId, token);

        if (result is null)
        {
            throw new AccountNotFoundException();
        }

        switch (type)
        {
            case BalanceChangeType.Withdrawal:
                result.Balance -= value;
                break;
            case BalanceChangeType.Deposit:
                result.Balance += value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown BalanceChangeType encountered.");
        }
        
        context.Accounts.Update(result);
        await context.SaveChangesAsync(token);
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