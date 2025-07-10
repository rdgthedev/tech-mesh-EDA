namespace TechMesh.Auth.Application.Abstracts.Adapters;

public interface IUserServiceApiRefitAdapter
{
    Task<Result<bool>> CreateUserAsync(CreateUserRequest request);
}