using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Notifications.Accounts;

public sealed class AccountUpdatedNotificationHandler(ILogger<AccountUpdatedNotificationHandler> logger)
    : IDomainNotificationHandler<AccountUpdatedNotification>
{
    public Task Handle(AccountUpdatedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Account {notification.Account.Id} updated.");
        
        return Task.CompletedTask;
    }
}