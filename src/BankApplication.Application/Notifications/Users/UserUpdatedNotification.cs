using BankApplication.Application.Mediatr;
using BankApplication.Core.Models;

namespace BankApplication.Application.Notifications.Users;

public sealed record UserUpdatedNotification(User User, Guid CorrelationId) : IDomainNotification;