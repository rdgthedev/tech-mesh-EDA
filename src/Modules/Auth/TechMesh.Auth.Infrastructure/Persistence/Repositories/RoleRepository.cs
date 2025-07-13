namespace TechMesh.Auth.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<List<Role>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => await _context
            .Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name.ToUpper() == name.ToUpper(), cancellationToken);

    public async Task CreateAsync(Role role, CancellationToken cancellationToken)
        => await _context.Roles.AddAsync(role, cancellationToken);

    public Task UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        _context.Roles.Update(role);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        _context.Roles.Remove(role);
        return Task.CompletedTask;
    }
}