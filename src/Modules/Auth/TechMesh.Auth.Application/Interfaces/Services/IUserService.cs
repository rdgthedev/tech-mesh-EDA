namespace TechMesh.Auth.Application.Contracts.Services;

public interface IUserService
{
    Task<Results> GetAllAsync(CancellationToken cancellationToken);
    Task<Results> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Results> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Results> GetUserWithRoleByEmailAsync(string email, CancellationToken cancellationToken);
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task<Results> ExistsAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken);
    Task<Results> UpdateAsync(UpdateUserRequest user, CancellationToken cancellationToken);
    Task<Results> DeactivateAsync(Guid id, CancellationToken cancellationToken);
    Task<Results> ActivateAsync(Guid id, CancellationToken cancellationToken);

    Task<Results> ChangeRoleAsync(
        ChangeUserRoleRequest changeUserRoleRequest,
        CancellationToken cancellationToken);
}