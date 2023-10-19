using MediatR;

namespace BankApplication.Application.Mediatr;

public interface ICommand<out T> : IRequest<T>
{
    public Guid CorrelationId { get; init; }
    public Guid SendingSystemId { get; init; }
}

public interface ICommand : IRequest
{
    public Guid CorrelationId { get; init; }
    public Guid SendingSystemId { get; init; }
}