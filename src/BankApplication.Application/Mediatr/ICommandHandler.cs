using MediatR;

namespace BankApplication.Application.Mediatr;

public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand;

public interface ICommandHandler<in T, TO> : IRequestHandler<T, TO>
    where T : ICommand<TO>
    where TO : class;