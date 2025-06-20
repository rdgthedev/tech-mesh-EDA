namespace TechMesh.Auth.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<RoleResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);

        if (!roles.Any())
            return Result<List<RoleResponse>>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Roles not found!");

        return Result<List<RoleResponse>>.Success(
            Convert.ToInt32(HttpStatusCode.OK),
            Mapper.Map(roles),
            "Roles found with success!");
    }

    public async Task<Result<RoleResponse?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            return Result<RoleResponse?>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role not found!");

        return Result<RoleResponse?>.Success(
            Convert.ToInt32(HttpStatusCode.OK),
            Mapper.Map(role),
            "Role found with success!");
    }

    public async Task<Result<RoleResponse>> GetByIdOrDefault(Guid? id, CancellationToken cancellationToken)
    {
        var roleResponse = id.HasValue && id != Guid.Empty
            ? await GetByIdAsync((Guid)id, cancellationToken)
            : await GetByNameAsync(nameof(User), cancellationToken);

        if (!roleResponse.IsSuccess)
            return Result<RoleResponse>.Failure(roleResponse.StatusCode, roleResponse.Errors.ToArray());

        return Result<RoleResponse>.Success(
            roleResponse.Data!,
            roleResponse.Message!);
    }

    public async Task<Result<RoleResponse?>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByNameAsync(name, cancellationToken);

        if (role is null)
            return Result<RoleResponse?>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role not found!");

        return Result<RoleResponse?>.Success(
            Mapper.Map(role),
            "Role found with success!");
    }

    public async Task<Result<bool>> CreateAsync(
        CreateRoleRequest createRoleRequest,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByNameAsync(createRoleRequest.Name, cancellationToken);

        if (role is not null)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role already exists!");

        await _roleRepository.CreateAsync(new Role(createRoleRequest.Name), cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<bool>.Success(Convert.ToInt32(HttpStatusCode.OK), true, "Role found with success!");
    }

    public async Task<Result<bool>> UpdateAsync(
        Guid id,
        UpdateRoleRequest updateRoleRequest,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role not found exists!");

        role.ChangeName(updateRoleRequest.Name);

        await _roleRepository.UpdateAsync(role, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<bool>.Success(Convert.ToInt32(HttpStatusCode.OK), true, "Role updated with success!");
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role not found exists!");

        await _roleRepository.DeleteAsync(role, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<bool>.Success(Convert.ToInt32(HttpStatusCode.OK), true, "Role deleted with success!");
    }
}