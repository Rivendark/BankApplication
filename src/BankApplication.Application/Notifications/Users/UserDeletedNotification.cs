using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Notifications.Users;

public sealed record UserDeletedNotification(Guid UserId, Guid CorrelationId) : IDomainNotification;