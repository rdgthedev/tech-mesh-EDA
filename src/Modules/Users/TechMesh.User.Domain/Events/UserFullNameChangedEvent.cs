namespace TechMesh.User.Domain.Events;

public class UserFullNameChangedEvent : Event
{
    public string FullName { get; private set; }

    public UserFullNameChangedEvent(Guid aggregateId, string fullName) : base(aggregateId) => FullName = fullName;
}