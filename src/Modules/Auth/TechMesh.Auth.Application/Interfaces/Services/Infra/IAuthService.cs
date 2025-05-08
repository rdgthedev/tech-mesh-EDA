namespace TechMesh.Auth.Application.Interfaces.Services.Infra;

public interface IAuthService
{
    // Task<Result<AuthTokensResponse>> RegisterAsync(
    //     RegisterUserRequest userRequest,
    //     CancellationToken cancellationToken);

    Task<Results> SignInAsync(
        SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken);
}