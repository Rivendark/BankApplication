using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Application.Repositories;

public interface IAccountRepository
{
    public Task CreateAccountAsync(Account account, CancellationToken token);
    public Task<Account> GetAccountAsync(Guid id, CancellationToken token);
    public Task DeleteAccountAsync(Guid id, CancellationToken token);
    public Task ApplyBalanceChangeAsync(BalanceChange balanceChange, CancellationToken token);
}