namespace TechMesh.User.Domain.Events;

public class UserEmailChangedEvent : Event
{
    public string Email { get; private set; }

    public UserEmailChangedEvent(Guid aggregateId, string email) : base(aggregateId)
        => Email = email;
}