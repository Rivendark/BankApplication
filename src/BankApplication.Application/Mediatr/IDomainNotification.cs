using MediatR;

namespace BankApplication.Application.Mediatr;

public interface IDomainNotification : INotification
{
    public Guid CorrelationId { get; init; }
}