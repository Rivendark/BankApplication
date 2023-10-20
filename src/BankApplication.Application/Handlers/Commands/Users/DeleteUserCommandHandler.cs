using BankApplication.Application.Commands.Users;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Users;
using BankApplication.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Commands.Users;

public sealed class DeleteUserCommandHandler(
        IUserRepository userRepository,
        IPublisher publisher,
        ILogger<DeleteUserCommandHandler> logger)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userRepository.DeleteUserAsync(request.UserId, cancellationToken);
            await publisher.Publish(
                new UserDeletedNotification(request.UserId, request.CorrelationId),
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"{GetType().Name}:{ex.Message}", request);

            throw;
        }
    }
}