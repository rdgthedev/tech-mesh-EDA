namespace TechMesh.User.Domain.Events.Technology;

public class TechnologyCreatedEvent : Event
{
    public string Name { get; private set; }

    public TechnologyCreatedEvent(Guid aggregateId, string name) : base(aggregateId)
        => Name = name;
}