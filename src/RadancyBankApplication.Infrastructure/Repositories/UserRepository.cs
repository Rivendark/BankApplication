using Microsoft.EntityFrameworkCore;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Core.Exceptions;
using RadancyBankApplication.Core.Models;
using RadancyBankApplication.Infrastructure.Contexts;
using RadancyBankApplication.Infrastructure.DBOs;

namespace RadancyBankApplication.Infrastructure.Repositories;

public class UserRepository(BankDbContext context) : IUserRepository
{
    public async Task CreateUserAsync(User user, CancellationToken token)
    {
        var existingUser = await FindUserAsync(user.Id, token);
        if (existingUser is not null)
        {
            throw new UserExistsException();
        }

        await context.Users.AddAsync(new UserDbo(user), token);
        await context.SaveChangesAsync(token);
    }

    public async Task<User?> GetUserAsync(Guid id, CancellationToken token)
    {
        var result = await FindUserAsync(id, token);

        return result?.ToDomainModel();
    }

    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken token)
    {
        return await context.Users.Select(x => x.ToDomainModel())
            .ToListAsync(token);
    }

    public async Task UpdateUserAsync(User user, CancellationToken token)
    {
        var existingUser = await FindUserAsync(user.Id, token);
        if (existingUser is null)
        {
            throw new UserNotFoundException();
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;

        context.Users.Update(existingUser);
        await context.SaveChangesAsync(token);
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken token)
    {
        var existingUser = await FindUserAsync(id, token);
        if (existingUser is null)
        {
            throw new UserNotFoundException();
        }

        context.Users.Remove(existingUser);
        await context.SaveChangesAsync(token);
    }

    private async Task<UserDbo?> FindUserAsync(Guid id, CancellationToken token)
    {
        return await context.Users.FindAsync(new object?[] { id }, cancellationToken: token);
    }
}