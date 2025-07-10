namespace TechMesh.Auth.Application.Abstracts.Services.Infra;

public interface IJwtService
{
    Task<string> GenerateAccessToken(Guid userId, string roleName);
}