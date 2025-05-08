namespace TechMesh.Auth.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Results> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        return !users.Any()
            ? Results.Failure((int)HttpStatusCode.NotFound, "Users not found!")
            : Results.Success(Mapper.Map(users), "Users found with success!");
    }

    public async Task<Results> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        return user is null
            ? Results.Failure((int)HttpStatusCode.NotFound, "Users not found!")
            : Results.Success(Mapper.Map(user), "User found with success!");
    }

    public async Task<Results> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(email, cancellationToken);

        return user is null
            ? Results.Failure((int)HttpStatusCode.NotFound, "User not found!")
            : Results.Success(Mapper.Map(user), "User found with success!");
    }

    public async Task<Results> GetUserWithRoleByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithRoleByEmailAsync(email, cancellationToken);

        return user is null
            ? Results.Failure((int)HttpStatusCode.NotFound, "User not found!")
            : Results.Success(Mapper.Map(user), "User found with success!");
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _userRepository.CreateAsync(user, cancellationToken);
    }

    public async Task<Results> ExistsAsync(
        Expression<Func<User, bool>> expression,
        CancellationToken cancellationToken)
    {
        var exists = await _userRepository.ExistsAsync(expression, cancellationToken);

        return exists
            ? Results.Success(exists)
            : Results.Failure("User not found!");
    }

    public async Task<Results> UpdateAsync(
        UpdateUserRequest updateUserRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(updateUserRequest.Id, cancellationToken);

        if (user is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "User not found!");

        user.UpdateEmail(updateUserRequest.Email);

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(true, "User updated with success!");
    }

    public async Task<Results> DeactivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "User not found!");

        user.Deactivate();

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(true, "User deactived with success!");
    }

    public async Task<Results> ActivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "User not found!");

        user.Activate();

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(true, "User actived with success!");
    }

    public async Task<Results> ChangeRoleAsync(
        ChangeUserRoleRequest changeUserRoleRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(changeUserRoleRequest.UserId, cancellationToken);

        if (user is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "User not found!");

        var role = await _roleRepository.GetByIdAsync(changeUserRoleRequest.RoleId, cancellationToken);

        if (role is null)
            return Results.Failure((int)HttpStatusCode.NotFound, "Role not found!");

        if (user.RoleId == role.Id)
            return Results.Failure((int)HttpStatusCode.BadRequest, "Role already assigned!");

        user.ChangeRole(role.Id);

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(true, "Role already assigned with success!");
    }
}