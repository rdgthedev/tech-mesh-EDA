namespace TechMesh.User.Domain.Events;

public class UserBirthDateChangedEvent : Event
{
    public DateTime BirthDate { get; private set; }

    public UserBirthDateChangedEvent(Guid aggregateId, DateTime birthDate) : base(aggregateId)
        => BirthDate = birthDate;
}