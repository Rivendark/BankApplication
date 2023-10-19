using BankApplication.Application.Commands.Users;
using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Users;
using BankApplication.Application.Repositories;
using BankApplication.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Commands.Users;

public sealed class CreateUserCommandHandler(
        IUserRepository userRepository,
        IPublisher publisher,
        ILogger<CreateUserCommandHandler> logger)
    : ICommandHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.CreateUserAsync(new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            }, cancellationToken);

            await publisher.Publish(new UserCreatedNotification(user, request.CorrelationId), cancellationToken);

            return new UserDto(user, request.CorrelationId);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"{GetType()}:{ex.Message}", request);

            throw;
        }
    }
}