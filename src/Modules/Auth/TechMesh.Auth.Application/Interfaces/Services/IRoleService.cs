namespace TechMesh.Auth.Application.Interfaces.Services;

public interface IRoleService
{
    Task<Result<List<RoleResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<RoleResponse?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<RoleResponse>> GetByIdOrDefault(Guid? id, CancellationToken cancellationToken);
    Task<Result<RoleResponse?>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Result<bool>> CreateAsync(CreateRoleRequest createRoleRequest, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateAsync(Guid id, UpdateRoleRequest updateRoleRequest, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
}