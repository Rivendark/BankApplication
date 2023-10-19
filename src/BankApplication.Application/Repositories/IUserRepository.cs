using BankApplication.Core.Models;

namespace BankApplication.Application.Repositories;

public interface IUserRepository
{
    public Task<User> CreateUserAsync(User user, CancellationToken token);
    public Task<User?> GetUserAsync(Guid id, CancellationToken token);
    public Task<IReadOnlyCollection<User>> GetUsersAsync(List<Guid> ids, CancellationToken token);
    public Task<IReadOnlyCollection<User>> GetUsersAsync(CancellationToken token); 
    public Task<User> UpdateUserAsync(User user, CancellationToken token);
    public Task DeleteUserAsync(Guid id, CancellationToken token);
}