namespace TechMesh.User.Infrastructure.Persistence.Repositories;

public class TechnologyRepository : ITechnologyRepository
{
    private readonly UserDbContext _context;

    public TechnologyRepository(UserDbContext context) => _context = context;

    public async Task<List<Technology>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Technologies.ToListAsync(cancellationToken);

    public async Task<List<Technology>?> GetByNamesAsync(List<string> technologiesNames,
        CancellationToken cancellationToken)
    {
        var technologies = await _context.Technologies
            .Where(t => technologiesNames.Contains(t.Name.Value.ToUpper()))
            .ToListAsync(cancellationToken);

        return technologies;
    }

    public async Task<Technology> CreateAsync(Technology technology, CancellationToken cancellationToken)
        => (await _context.AddAsync(technology, cancellationToken)).Entity;

    public async Task<List<Technology>> CreateAsync(List<Technology> technologies, CancellationToken cancellationToken)
    {
        await _context.AddRangeAsync(technologies, cancellationToken);

        return technologies;
    }
}