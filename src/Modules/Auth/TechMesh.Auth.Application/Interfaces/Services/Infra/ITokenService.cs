namespace TechMesh.Auth.Application.Interfaces.Services.Infra;

public interface ITokenService
{
    Task<Token> CreateAsync(Token token, CancellationToken cancellationToken);

    Task<Results> DeleteAsync(string token, CancellationToken cancellationToken);
}