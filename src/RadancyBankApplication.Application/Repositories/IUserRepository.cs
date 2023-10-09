using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Application.Repositories;

public interface IUserRepository
{
    public Task CreateUserAsync(User user, CancellationToken token);
    public Task<User?> GetUserAsync(Guid id, CancellationToken token);
    public Task<IEnumerable<User>> GetUsersAsync(CancellationToken token); 
    public Task UpdateUserAsync(User user, CancellationToken token);
    public Task DeleteUserAsync(Guid id, CancellationToken token);
}