namespace TechMesh.Auth.Domain.Interfaces.Repositories;

public interface ITokenRepository
{
    Task<Token?> GetByTokenAsync(string token, CancellationToken cancellationToken);
    Task<Token?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(Token token, CancellationToken cancellationToken);
    Task DeleteAsync(Token token, CancellationToken cancellationToken);
}