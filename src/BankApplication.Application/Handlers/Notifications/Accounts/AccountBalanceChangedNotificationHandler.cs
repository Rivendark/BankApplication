using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Notifications.Accounts;

public sealed class AccountBalanceChangedNotificationHandler(ILogger<AccountBalanceChangedNotificationHandler> logger)
    : IDomainNotificationHandler<AccountBalanceChangedNotification>
{
    public Task Handle(AccountBalanceChangedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Account {notification.balanceChange.AccountId} balance changed.");

        return Task.CompletedTask;
    }
}