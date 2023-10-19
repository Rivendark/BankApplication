using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Users;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Notifications.Users;

public sealed class UserDeletedNotificationHandler(ILogger<UserDeletedNotificationHandler> logger)
    : IDomainNotificationHandler<UserDeletedNotification>
{
    public Task Handle(UserDeletedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"User {notification.UserId} deleted.");

        return Task.CompletedTask;
    }
}