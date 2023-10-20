using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Queries.Users;
using BankApplication.Application.Repositories;
using BankApplication.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Queries.Users;

public sealed class GetUserQueryHandler(IUserRepository userRepository, ILogger<GetUserQueryHandler> logger)
    : IQueryHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
            var user = await userRepository.GetUserAsync(request.UserId, cancellationToken);
            if (user is not null)
            {
                return new UserDto(user, request.CorrelationId);
            }
            
            var ex = new UserNotFoundException();
            logger.LogInformation($"{GetType().Name}:{ex.Message}", request);
            throw ex;
    }
}