namespace TechMesh.User.Domain.Interfaces.Repositories;

public interface ITechnologyRepository
{
    Task<List<Technology>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<Technology>?> GetByNamesAsync(List<string> technologiesNames, CancellationToken cancellationToken);
    Task<Technology> CreateAsync(Technology technology, CancellationToken cancellationToken);
    Task<List<Technology>> CreateAsync(List<Technology> technologies, CancellationToken cancellationToken);
}