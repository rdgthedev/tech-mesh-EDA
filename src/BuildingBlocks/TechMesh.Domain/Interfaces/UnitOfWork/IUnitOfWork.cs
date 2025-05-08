namespace TechMesh.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}