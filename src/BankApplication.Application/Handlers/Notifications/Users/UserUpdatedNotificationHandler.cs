using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Users;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Notifications.Users;

public sealed class UserUpdatedNotificationHandler(ILogger<UserUpdatedNotificationHandler> logger)
    : IDomainNotificationHandler<UserUpdatedNotification>
{
    public Task Handle(UserUpdatedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"User {notification.User.Id} updated.");
        
        return Task.CompletedTask;
    }
}