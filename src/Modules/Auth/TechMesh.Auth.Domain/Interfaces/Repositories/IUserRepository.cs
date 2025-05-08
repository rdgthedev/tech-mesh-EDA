namespace TechMesh.Auth.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<User?> GetUserWithRoleByEmailAsync(string email, CancellationToken cancellationToken);
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user);
    Task<bool> ExistsAsync(Expression<Func<User, bool>> func, CancellationToken cancellationToken);
}