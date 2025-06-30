namespace TechMesh.User.Domain.Events.User;

public class UserAddressChangedEvent : Event
{
    public string Address { get; private set; }

    public UserAddressChangedEvent(Guid aggregateId, string address) : base(aggregateId)
        => Address = address;
}