using TechMesh.Auth.Application.Interfaces.Services;

namespace TechMesh.Auth.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Results> GetAllAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);

        if (!roles.Any())
            return Results.Failure((int)HttpStatusCode.NotFound, "Roles not found!");

        return Results.Success(Mapper.Map(roles), "Roles found with success!");
    }

    public async Task<Results> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "Role not found!");

        return Results.Success(Mapper.Map(role), "Role found with success!");
    }

    public async Task<Results> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByNameAsync(name, cancellationToken);

        if (role is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "Role not found!");

        return Results.Success(Mapper.Map(role), "Role found with success!");
    }

    public async Task<Results> CreateAsync(
        CreateRoleRequest createRoleRequest,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByNameAsync(createRoleRequest.Name, cancellationToken);

        if (role is not null)
            return Results.Failure((int)HttpStatusCode.NotFound, "Role already exists!");

        await _roleRepository.CreateAsync(new Role(createRoleRequest.Name), cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(true, "Role found with success!");
    }

    public async Task<Results> UpdateAsync(
        Guid id,
        UpdateRoleRequest updateRoleRequest,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "Role not found exists!");

        role.ChangeName(updateRoleRequest.Name);

        await _roleRepository.UpdateAsync(role, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(true, "Role updated with success!");
    }

    public async Task<Results> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "Role not found exists!");

        await _roleRepository.DeleteAsync(role, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(true, "Role deleted with success!");
    }
}