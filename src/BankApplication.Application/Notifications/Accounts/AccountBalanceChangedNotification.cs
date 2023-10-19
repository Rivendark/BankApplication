using BankApplication.Application.Mediatr;
using BankApplication.Core.Models;

namespace BankApplication.Application.Notifications.Accounts;

public sealed record AccountBalanceChangedNotification(BalanceChange balanceChange, Guid CorrelationId) : IDomainNotification;