namespace TechMesh.Auth.Application.Interfaces.Services.Infra;

public interface IJwtService
{
    string GenerateAccessToken(Guid userId, string roleName);
    Token GenerateRefreshToken(Guid userId, DateTime expirationTime);
    Results ValidateToken(string token);
}