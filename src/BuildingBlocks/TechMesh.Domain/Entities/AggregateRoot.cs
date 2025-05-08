namespace TechMesh.Domain.Entities;

public abstract class AggregateRoot : Entity
{
    private readonly List<Event> _events = [];
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    public void AddEvent(Event? @event)
    {
        DomainException.When(@event is null, "Domain event cannot be null.");

        _events.Add(@event!);
    }

    public void ClearEvents()
    {
        DomainException.When(!_events.Any(), "No domain events to clear.");

        _events.Clear();
    }
}