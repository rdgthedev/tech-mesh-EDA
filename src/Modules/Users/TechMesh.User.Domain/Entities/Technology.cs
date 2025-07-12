namespace TechMesh.User.Domain.Entities;

public class Technology : AggregateRoot
{
    public TechnologyName Name { get; private set; } = null!;
    public IReadOnlyCollection<UserTechnology> Users { get; private set; } = null!;

    private Technology()
    {
    }

    private Technology(TechnologyName name)
    {
        Name = name;

        // AddEvent(new TechnologyCreatedEvent(Id, Name));
    }

    public static Technology Create(TechnologyName name)
    {
        Validate(name);

        return new Technology(name);
    }

    private static void Validate(TechnologyName? name)
        => DomainException.When(name is null, $"The {nameof(TechnologyName)} object cannot be null.");

    public void Update(TechnologyName name)
    {
        DomainException.When(!string.IsNullOrEmpty(name.Value), "Name is required.");

        Name = name;
    }

    public override string ToString() => Name.Value;
}