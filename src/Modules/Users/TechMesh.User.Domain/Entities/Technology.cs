namespace TechMesh.User.Domain.Entities;

public class Technology : Entity
{
    public TechnologyName Name { get; private set; } = null!;
    public IReadOnlyCollection<User> Users { get; private set; } = null!;

    private Technology()
    {
    }

    private Technology(TechnologyName name) => Name = name;

    public static Technology Create(TechnologyName name) => new(name);

    public void Update(TechnologyName name)
    {
        DomainException.When(!string.IsNullOrEmpty(name.Value), "Name is required.");

        Name = name;
    }

    public override string ToString() => Name.Value;
}