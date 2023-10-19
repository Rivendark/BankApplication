using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Queries.Users;
using BankApplication.Application.Repositories;
using BankApplication.Core.Models;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Queries.Users;

public sealed class GetUsersQueryHandler(IUserRepository userRepository, ILogger<GetUsersQueryHandler> logger)
    : IQueryHandler<GetUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await GetUsersAsync(request.UserIds, cancellationToken);
        logger.LogDebug($"{typeof(GetUsersQuery)}: Found {users.Count} users.");
        return users.Select(x => new UserDto(x)).ToList();
    }
    
    private async Task<IReadOnlyCollection<User>> GetUsersAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        if (ids.Count != 0)
        {
            return await userRepository.GetUsersAsync(ids.ToList(), cancellationToken);
        }

        return await userRepository.GetUsersAsync(cancellationToken);
    }
}