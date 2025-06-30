namespace TechMesh.User.Domain.Events.User;

public class UserPhoneNumberChangedEvent : Event
{
    public string PhoneNumber { get; private set; }

    public UserPhoneNumberChangedEvent(Guid aggregateId, string phoneNumber) : base(aggregateId)
        => PhoneNumber = phoneNumber;
}