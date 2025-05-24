namespace TechMesh.Auth.Application.Interfaces.Services.Infra;

public interface IJwtService
{
    Task<string> GenerateAccessToken(Guid userId, string roleName);
    Task<Result<TokenResponse>> GenerateRefreshToken(Guid userId, CancellationToken cancellationToken);
}