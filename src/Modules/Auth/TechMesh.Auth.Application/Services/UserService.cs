using TechMesh.Auth.Application.Abstracts.Services.User;

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

    public async Task<Result<List<UserDetailsResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        return !users.Any()
            ? Result<List<UserDetailsResponse>>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Users not found!")
            : Result<List<UserDetailsResponse>>.Success(Convert.ToInt32(HttpStatusCode.OK), Mapper.Map(users),
                "Users found with success!");
    }

    public async Task<Result<UserDetailsResponse?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        return user is null
            ? Result<UserDetailsResponse?>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Users not found!")
            : Result<UserDetailsResponse?>.Success(Convert.ToInt32(HttpStatusCode.OK), Mapper.Map(user),
                "User found with success!");
    }

    public async Task<Result<UserDetailsResponse?>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(email, cancellationToken);

        return user is null
            ? Result<UserDetailsResponse?>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "User not found!")
            : Result<UserDetailsResponse?>.Success(Convert.ToInt32(HttpStatusCode.OK), Mapper.Map(user),
                "User found with success!");
    }

    public async Task<Result<UserDetailsResponse?>> GetUserWithRoleByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithRoleByEmailAsync(email, cancellationToken);

        return user is null
            ? Result<UserDetailsResponse?>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "User not found!")
            : Result<UserDetailsResponse?>.Success(
                Convert.ToInt32(HttpStatusCode.OK),
                Mapper.Map(user),
                "User found with success!");
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
        => await _userRepository.CreateAsync(user, cancellationToken);


    public async Task<Result<bool>> ExistsAsync(
        Expression<Func<User, bool>> expression,
        CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ExistsAsync(expression, cancellationToken);

        return !userExists
            ? Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "User not found!")
            : Result<bool>.Success(Convert.ToInt32(HttpStatusCode.OK), userExists, "User already exists!");
    }

    public async Task<Result<Guid>> UpdateAsync(
        UpdateUserRequest updateUserRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(updateUserRequest.Id, cancellationToken);

        if (user is null)
            return Result<Guid>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "User not found!");

        user.UpdateEmail(updateUserRequest.Email);

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<Guid>.Success(Convert.ToInt32(HttpStatusCode.OK), user.Id, "User updated with success!");
    }

    public async Task<Result<bool>> DeactivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "User not found!");

        user.Deactivate();

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<bool>.Success(Convert.ToInt32(HttpStatusCode.OK), true, "User deactived with success!");
    }

    public async Task<Result<bool>> ActivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "User not found!");

        user.Activate();

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<bool>.Success(Convert.ToInt32(HttpStatusCode.OK), true, "User actived with success!");
    }

    public async Task<Result<bool>> ChangeRoleAsync(
        ChangeUserRoleRequest changeUserRoleRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(changeUserRoleRequest.UserId, cancellationToken);

        if (user is null)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "User not found!");

        var role = await _roleRepository.GetByIdAsync(changeUserRoleRequest.RoleId, cancellationToken);

        if (role is null)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role not found!");

        if (user.RoleId == role.Id)
            return Result<bool>.Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "Role already assigned!");

        user.ChangeRole(role.Id);

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<bool>.Success(Convert.ToInt32(HttpStatusCode.OK), true, "Role already assigned with success!");
    }
}