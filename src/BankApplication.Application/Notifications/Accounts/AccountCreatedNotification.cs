using BankApplication.Application.Mediatr;
using BankApplication.Core.Models;

namespace BankApplication.Application.Notifications.Accounts;

public sealed record AccountCreatedNotification(Account Account, Guid CorrelationId) : IDomainNotification;