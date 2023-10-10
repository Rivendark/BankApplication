using RadancyBankApplication.Application.Services;
using RadancyBankApplication.Core.Enums;
using RadancyBankApplication.Core.Exceptions;
using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.UnitTests.Services;

[TestFixture]
public class AccountValidationServiceTests
{
    private AccountValidationService _service;
    private Account _account;

    [SetUp]
    public void BeforeEachTest()
    {
        _service = new AccountValidationService();
        _account = new ()
        {
            Balance = 500,
            CreatedAtUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Name = "Test Account",
            UserId = Guid.NewGuid()
        };
    }

    [Test]
    public void Should_Throw_Exception_On_Excessive_Deposit()
    {
        var balanceChange = new BalanceChange
        {
            AccountId = Guid.NewGuid(),
            Amount = 50000,
            CreatedAtUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Type = BalanceChangeType.Deposit,
            UserId = Guid.NewGuid()
        };
        
        Assert.Throws<DepositLimitExceededException>(() => _service.ValidateDeposit(_account, balanceChange));
    }
    
    [Test]
    public void Should_Not_Throw_Exception_On_Deposit()
    {
        var balanceChange = new BalanceChange
        {
            AccountId = Guid.NewGuid(),
            Amount = 50,
            CreatedAtUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Type = BalanceChangeType.Deposit,
            UserId = Guid.NewGuid()
        };

        Assert.DoesNotThrow(() => _service.ValidateDeposit(_account, balanceChange));
    }

    [Test]
    public void Should_Not_Throw_On_Withdrawal()
    {
        var balanceChange = new BalanceChange
        {
            AccountId = Guid.NewGuid(),
            Amount = 50,
            CreatedAtUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Type = BalanceChangeType.Withdrawal,
            UserId = Guid.NewGuid()
        };
        
        Assert.DoesNotThrow(() => _service.ValidateWithdrawal(_account, balanceChange));
    }
    
    [Test]
    public void Should_Throw_On_Min_Account_Balance_Withdrawal()
    {
        _account.Balance = 200;
        var balanceChange = new BalanceChange
        {
            AccountId = Guid.NewGuid(),
            Amount = 110,
            CreatedAtUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Type = BalanceChangeType.Withdrawal,
            UserId = Guid.NewGuid()
        };
        
        Assert.Throws<InsufficientAccountBalanceException>(() => _service.ValidateWithdrawal(_account, balanceChange));
    }
    
    [Test]
    public void Should_Throw_On_Balance_Percent_Exceeded_Withdrawal()
    {
        _account.Balance = 10000;
        var balanceChange = new BalanceChange
        {
            AccountId = Guid.NewGuid(),
            Amount = 9001,
            CreatedAtUtc = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Type = BalanceChangeType.Withdrawal,
            UserId = Guid.NewGuid()
        };
        
        Assert.Throws<WithdrawalPercentageExceededException>(() => _service.ValidateWithdrawal(_account, balanceChange));
    }
}