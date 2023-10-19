using BankApplication.Core.Enums;
using BankApplication.Core.Exceptions;
using BankApplication.Core.Models;

namespace BankApplication.Application.Services;

public class AccountValidationService : IAccountValidationService
{
    private readonly Dictionary<BalanceChangeType, List<Action<Account, BalanceChange>>> _rules = new();

    public AccountValidationService()
    {
        _rules[BalanceChangeType.Deposit] = new List<Action<Account, BalanceChange>>
        {
            ValidateDepositMaximumValue
        };

        _rules[BalanceChangeType.Withdrawal] = new List<Action<Account, BalanceChange>>
        {
            ValidateWithdrawalAccountMinimum,
            ValidateWithdrawalAccountBalancePercent
        };
    }

    public void ValidateDeposit(Account account, BalanceChange balanceChange)
    {
        Validate(account, balanceChange, BalanceChangeType.Deposit);
    }

    public void ValidateWithdrawal(Account account, BalanceChange balanceChange)
    {
        Validate(account, balanceChange, BalanceChangeType.Withdrawal);
    }

    private void Validate(Account account, BalanceChange balanceChange, BalanceChangeType type)
    {
        foreach (var rule in _rules[type])
        {
            rule(account, balanceChange);
        }
    }

    private void ValidateWithdrawalAccountMinimum(Account account, BalanceChange balanceChange)
    {
        if (account.Balance - balanceChange.Amount <= 100)
        {
            throw new InsufficientAccountBalanceException();
        }
    }

    private void ValidateWithdrawalAccountBalancePercent(Account account, BalanceChange balanceChange)
    {
        if (balanceChange.Amount / account.Balance > (decimal)0.9)
        {
            throw new WithdrawalPercentageExceededException();
        }
    }

    private void ValidateDepositMaximumValue(Account _, BalanceChange balanceChange)
    {
        if (balanceChange.Amount > 10000)
        {
            throw new DepositLimitExceededException();
        }
    }
}