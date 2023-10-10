using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Application.Services;

public interface IAccountValidationService
{
    public void ValidateDeposit(Account account, BalanceChange balanceChange);
    public void ValidateWithdrawal(Account account, BalanceChange balanceChange);
}