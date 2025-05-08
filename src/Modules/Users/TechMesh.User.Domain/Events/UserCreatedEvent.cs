namespace TechMesh.User.Domain.Events;

public class UserCreatedEvent : Event
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public List<Technology> Skills { get; private set; }
    public EUserStatus Status { get; private set; }

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
        List<Technology> skills,
        EUserStatus status) : base(aggregateId)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Address = address;
        Skills = skills;
        Status = status;
    }
}