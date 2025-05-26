namespace TechMesh.User.Domain.Entities;

public class UserTechnology
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid TechnologyId { get; private set; }
    public Technology Technology { get; private set; } = null!;
}