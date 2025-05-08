namespace TechMesh.Auth.Domain.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync(CancellationToken cancellationToken);
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task CreateAsync(Role role, CancellationToken cancellationToken);
    Task UpdateAsync(Role role, CancellationToken cancellationToken);
    Task DeleteAsync(Role role, CancellationToken cancellationToken);
}