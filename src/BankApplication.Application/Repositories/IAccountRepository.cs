using BankApplication.Core.Enums;
using BankApplication.Core.Models;

namespace BankApplication.Application.Repositories;

public interface IAccountRepository
{
    public Task<Account> CreateAccountAsync(Account account, CancellationToken token);
    public Task<List<Account>> GetUserAccountsAsync(Guid userId, CancellationToken token);
    public Task<Account?> GetAccountAsync(Guid id, CancellationToken token);
    public Task<Account> UpdateAccountInformationAsync(Account account, CancellationToken token);
    public Task<Account> UpdateAccountBalanceAsync(Account account, BalanceChange balanceChange, CancellationToken token);
    public Task DeleteAccountAsync(Guid id, CancellationToken token);
    public Task<List<BalanceChange>> GetBalanceChangesAsync(Guid accountId, CancellationToken token);
}