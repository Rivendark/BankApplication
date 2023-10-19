using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Notifications.Accounts;

public sealed class AccountCreatedNotificationHandler(ILogger<AccountCreatedNotificationHandler> logger)
    : IDomainNotificationHandler<AccountCreatedNotification>
{
    public Task Handle(AccountCreatedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Account {notification.Account.Id} created.");
        
        return Task.CompletedTask;
    }
}