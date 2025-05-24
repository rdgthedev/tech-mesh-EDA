namespace TechMesh.Auth.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<List<UserDetailsResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<UserDetailsResponse?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<UserDetailsResponse?>> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Result<UserDetailsResponse?>> GetUserWithRoleByEmailAsync(string email, CancellationToken cancellationToken);
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task<Result<bool>> ExistsAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken);
    Task<Result<Guid>> UpdateAsync(UpdateUserRequest user, CancellationToken cancellationToken);
    Task<Result<bool>> DeactivateAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<bool>> ActivateAsync(Guid id, CancellationToken cancellationToken);

    Task<Result<bool>> ChangeRoleAsync(
        ChangeUserRoleRequest changeUserRoleRequest,
        CancellationToken cancellationToken);
}