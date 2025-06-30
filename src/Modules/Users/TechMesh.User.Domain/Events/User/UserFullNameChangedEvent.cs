namespace TechMesh.User.Domain.Events.User;

public class UserFullNameChangedEvent : Event
{
    public string FullName { get; private set; }

    public UserFullNameChangedEvent(Guid aggregateId, string fullName) : base(aggregateId) => FullName = fullName;
}