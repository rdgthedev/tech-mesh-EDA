namespace TechMesh.Domain.Events;

public abstract class Event
{
    public Guid AggregateId { get; private set; }

    protected Event()
    {
    }

    protected Event(Guid aggregateId) => AggregateId = aggregateId;
}