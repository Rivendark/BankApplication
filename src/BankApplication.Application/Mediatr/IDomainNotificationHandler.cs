using MediatR;

namespace BankApplication.Application.Mediatr;

public interface IDomainNotificationHandler<in T> : INotificationHandler<T> where T : IDomainNotification;