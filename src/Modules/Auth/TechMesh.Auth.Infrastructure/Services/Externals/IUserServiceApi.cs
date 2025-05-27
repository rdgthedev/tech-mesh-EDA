using Refit;
using TechMesh.Auth.Infrastructure.Services.Externals.Request;

namespace TechMesh.Auth.Infrastructure.Services.Externals;

public interface IUserServiceApi
{
    [Post("/users")]
    Task<Result<string>> CreateUser([Body] CreateUserBody body);
}

