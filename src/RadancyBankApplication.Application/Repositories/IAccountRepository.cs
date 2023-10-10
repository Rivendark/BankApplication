using RadancyBankApplication.Core.Enums;
using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Application.Repositories;

public interface IAccountRepository
{
    public Task CreateAccountAsync(Account account, CancellationToken token);
    public Task<List<Account>> GetUserAccountsAsync(Guid userId, CancellationToken token);
    public Task<Account?> GetAccountAsync(Guid id, CancellationToken token);
    public Task UpdateAccountInformationAsync(Account account, CancellationToken token);
    public Task UpdateAccountBalanceAsync(Guid accountId, decimal value, BalanceChangeType type, CancellationToken token);
    public Task DeleteAccountAsync(Guid id, CancellationToken token);
    public Task SaveBalanceChangeAsync(BalanceChange balanceChange, CancellationToken token);
    public Task<List<BalanceChange>> GetBalanceChangesAsync(Guid accountId, CancellationToken token);
}