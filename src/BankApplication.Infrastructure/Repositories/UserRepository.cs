using Microsoft.EntityFrameworkCore;
using BankApplication.Application.Repositories;
using BankApplication.Core.Exceptions;
using BankApplication.Core.Models;
using BankApplication.Infrastructure.Contexts;
using BankApplication.Infrastructure.DBOs;

namespace BankApplication.Infrastructure.Repositories;

public sealed class UserRepository(BankDbContext context) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user, CancellationToken token)
    {
        var existingUser = await FindUserAsync(user.Id, token);
        if (existingUser is not null)
        {
            throw new UserExistsException();
        }

        var dbo = new UserDbo(user);
        await context.Users.AddAsync(dbo, token);
        await context.SaveChangesAsync(token);

        return dbo.ToDomainModel();
    }

    public async Task<User?> GetUserAsync(Guid id, CancellationToken token)
    {
        var result = await FindUserAsync(id, token);

        return result?.DeletedAtUtc != null ? null : result.ToDomainModel();
    }

    public async Task<IReadOnlyCollection<User>> GetUsersAsync(List<Guid> ids, CancellationToken token)
    {
        var users = context.Users.Where(x => ids.Contains(x.Id));
        if (!users.Any())
        {
            return new List<User>();
        }
        
        return await users.Select(x => x.ToDomainModel())
            .ToListAsync(token);
    }

    public async Task<IReadOnlyCollection<User>> GetUsersAsync(CancellationToken token)
    {
        if (!context.Users.Any())
        {
            return new List<User>();
        }
        
        return await context.Users.Select(x => x.ToDomainModel())
            .ToListAsync(token);
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken token)
    {
        var result = await FindUserAsync(user.Id, token);
        if (result is null)
        {
            throw new UserNotFoundException();
        }

        result.FirstName = user.FirstName;
        result.LastName = user.LastName;
        result.Email = user.Email;

        context.Users.Update(result);
        await context.SaveChangesAsync(token);

        return result.ToDomainModel();
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken token)
    {
        var existingUser = await FindUserAsync(id, token);
        if (existingUser is null)
        {
            throw new UserNotFoundException();
        }
        
        existingUser.DeletedAtUtc = DateTime.UtcNow;
        context.Users.Update(existingUser);
        await context.SaveChangesAsync(token);
    }

    private async Task<UserDbo?> FindUserAsync(Guid id, CancellationToken token)
    {
        return await context.Users.FindAsync(new object?[] { id }, cancellationToken: token);
    }
}