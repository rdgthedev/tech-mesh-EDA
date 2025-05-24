namespace TechMesh.Auth.Application.Interfaces.Services.Infra;

public interface ITokenService
{
    Task<Result<TokenResponse>> CreateAsync(Token token, CancellationToken cancellationToken);

    Task<Result<bool>> DeleteAsync(string token, CancellationToken cancellationToken);
}