using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Notifications.Accounts;

public sealed class AccountDeletedNotificationHandler(ILogger<AccountDeletedNotificationHandler> logger)
    : IDomainNotificationHandler<AccountDeletedNotification>
{
    public Task Handle(AccountDeletedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Account {notification.AccountId} deleted.");
        
        return Task.CompletedTask;
    }
}