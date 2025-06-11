namespace TechMesh.Auth.Infrastructure.Services.Externals.Interfaces;

public interface IUserServiceApi
{
    [Post("/v1/users")]
    Task<HttpResponseMessage> CreateUserAsync([Body] CreateUserRequest request);
}