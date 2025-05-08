namespace TechMesh.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork()
    {
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}