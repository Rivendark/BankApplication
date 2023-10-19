using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Users;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Notifications.Users;

public sealed class UserCreatedNotificationHandler(ILogger<UserCreatedNotificationHandler> logger)
    : IDomainNotificationHandler<UserCreatedNotification>
{
    public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"User {notification.User.Id} created.");

        return Task.CompletedTask;
    }
}