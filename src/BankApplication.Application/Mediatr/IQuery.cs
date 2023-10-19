using MediatR;

namespace BankApplication.Application.Mediatr;

public interface IQuery<out T> : IRequest<T>
{
    public Guid CorrelationId { get; init; }
}

public interface IQuery : IRequest
{
    public Guid CorrelationId { get; init; }
}