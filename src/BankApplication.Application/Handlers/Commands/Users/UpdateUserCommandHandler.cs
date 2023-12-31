﻿using BankApplication.Application.Commands.Users;
using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Users;
using BankApplication.Application.Repositories;
using BankApplication.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Commands.Users;

public sealed class UpdateUserCommandHandler(
        IUserRepository userRepository,
        IPublisher publisher,
        ILogger<UpdateUserCommandHandler> logger)
    : ICommandHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.UpdateUserAsync(new User
            {
                Id = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            }, cancellationToken);

            await publisher.Publish(new UserUpdatedNotification(user, request.CorrelationId), cancellationToken);

            return new UserDto(user, request.CorrelationId);
        }
        catch (Exception ex)
        { 
            logger.LogInformation($"{GetType().Name}:{ex.Message}", request);

            throw;
        }
    }
}