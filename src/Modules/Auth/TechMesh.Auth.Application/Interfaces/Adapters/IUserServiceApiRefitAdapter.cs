namespace TechMesh.Auth.Application.Interfaces.Adapters;

public interface IUserServiceApiRefitAdapter
{
    Task<Result<bool>> CreateUserAsync(CreateUserRequest request);
}