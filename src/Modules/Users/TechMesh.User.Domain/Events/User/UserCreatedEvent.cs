namespace TechMesh.User.Domain.Events.User;

public class UserCreatedEvent : Event, INotification
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public List<UserTechnology> Skills { get; private set; }
    public EUserStatus Status { get; private set; }
    public EUserLevel Level { get; private set; }

    private UserCreatedEvent()
    {
    }

    public UserCreatedEvent(
        Guid aggregateId,
        string fullName,
        string email,
        DateTime birthDate,
        string phoneNumber,
        Address address,
        List<UserTechnology> skills,
        EUserStatus status,
        EUserLevel level) : base(aggregateId)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Address = address;
        Skills = skills;
        Status = status;
        Level = level;
    }
}