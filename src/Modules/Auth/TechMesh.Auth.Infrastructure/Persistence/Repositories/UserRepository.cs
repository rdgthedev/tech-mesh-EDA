

namespace TechMesh.Auth.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Users.ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<User?> GetUserWithRoleByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
        => await _context.Users.AddAsync(user, cancellationToken);

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Expression<Func<User, bool>> func, CancellationToken cancellationToken)
        => await _context.Users.AnyAsync(func, cancellationToken);
}