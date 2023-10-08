using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public async Task CreateUserAsync(User user, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserAsync(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}