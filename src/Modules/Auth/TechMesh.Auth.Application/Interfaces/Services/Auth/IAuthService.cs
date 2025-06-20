namespace TechMesh.Auth.Application.Interfaces.Services.Auth;

public interface IAuthService
{
    Task<Result<AuthTokensResponse>> RegisterAsync(
        RegisterUserRequest userRequest,
        CancellationToken cancellationToken);

    Task<Result<AuthTokensResponse>> SignInAsync(
        SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken);
}