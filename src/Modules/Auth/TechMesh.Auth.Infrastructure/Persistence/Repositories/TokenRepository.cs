namespace TechMesh.Auth.Infrastructure.Persistence.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly ApplicationDbContext _context;

    public TokenRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<Token?> GetByTokenAsync(string token, CancellationToken cancellationToken)
        => await _context.Tokens.FirstOrDefaultAsync(t => t.Value.ToString() == token, cancellationToken);

    public async Task<Token?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        => await _context.Tokens.FirstOrDefaultAsync(t => t.UserId == userId, cancellationToken);

    public async Task CreateAsync(Token token, CancellationToken cancellationToken)
        => await _context.Tokens.AddAsync(token, cancellationToken);

    public async Task DeleteAsync(Token token, CancellationToken cancellationToken)
    {
        _context.Tokens.Remove(token);
        await Task.CompletedTask;
    }
}