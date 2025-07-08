namespace TechMesh.Auth.Application.Interfaces.Services.Infra;

public interface IJwtService
{
    Task<string> GenerateAccessToken(Guid userId, string roleName);
}