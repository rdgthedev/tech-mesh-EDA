namespace TechMesh.Auth.Application.Interfaces.Services;

public interface IRoleService
{
    Task<Results> GetAllAsync(CancellationToken cancellationToken);
    Task<Results> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Results> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Results> CreateAsync(CreateRoleRequest createRoleRequest, CancellationToken cancellationToken);
    Task<Results> UpdateAsync(Guid id, UpdateRoleRequest updateRoleRequest, CancellationToken cancellationToken);
    Task<Results> DeleteAsync(Guid id, CancellationToken cancellationToken);
}