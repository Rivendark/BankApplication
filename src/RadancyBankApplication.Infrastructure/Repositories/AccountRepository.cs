using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    public async Task CreateAccountAsync(Account account, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> GetAccountAsync(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAccountAsync(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task ApplyBalanceChangeAsync(BalanceChange balanceChange, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}