using BankApplication.Core.Models;

namespace BankApplication.Application.Services;

public interface IAccountValidationService
{
    public void ValidateDeposit(Account account, BalanceChange balanceChange);
    public void ValidateWithdrawal(Account account, BalanceChange balanceChange);
}