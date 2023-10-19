using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Notifications.Accounts;

public sealed record AccountDeletedNotification(Guid AccountId, Guid CorrelationId) : IDomainNotification;