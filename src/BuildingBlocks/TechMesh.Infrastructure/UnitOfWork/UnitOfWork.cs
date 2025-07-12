namespace TechMesh.Infrastructure.UnitOfWork;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;

    public UnitOfWork(TContext context)
        => _context = context;

    public async Task CommitAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);
}