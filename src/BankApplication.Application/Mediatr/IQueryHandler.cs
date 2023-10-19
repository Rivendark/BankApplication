using MediatR;

namespace BankApplication.Application.Mediatr;

public interface IQueryHandler<in T> : IRequestHandler<T> where T : IQuery;

public interface IQueryHandler<in T, TO> : IRequestHandler<T, TO>
    where T : IQuery<TO>
    where TO : class;