namespace TechMesh.User.Domain.Events.User;

public class UserDeactivedEvent : Event
{
    public EUserStatus Status { get; private set; }

    public UserDeactivedEvent(Guid aggregateId, EUserStatus status) : base(aggregateId) => Status = status;
}