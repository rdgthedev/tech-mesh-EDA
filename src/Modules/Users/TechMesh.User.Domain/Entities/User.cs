namespace TechMesh.User.Domain.Entities;

public class User : AggregateRoot
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public EUserStatus Status { get; private set; }
    public EUserLevel Level { get; private set; }
    public Address Address { get; private set; }
    public List<UserTechnology> Technologies { get; private set; }

    private User()
    {
    }

    private User(
        FullName fullName,
        Email email,
        BirthDate birthDate,
        PhoneNumber phoneNumber,
        Address address,
        EUserLevel level)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Address = address;
        Technologies = [];
        Status = EUserStatus.Inactive;
        Level = level;

        AddEvent(new UserCreatedEvent(
            Id,
            FullName.Value,
            Email.Address,
            BirthDate.Value,
            PhoneNumber.Value,
            Address,
            Technologies,
            Status,
            Level));
    }

    public static User Create(
        FullName fullName,
        Email email,
        BirthDate birthDate,
        PhoneNumber phoneNumber,
        Address address,
        EUserLevel level)
    {
        Validate(fullName, email, birthDate, phoneNumber, address);

        return new User(fullName, email, birthDate, phoneNumber, address, level);
    }

    private static void Validate(
        FullName? fullName,
        Email? email,
        BirthDate? birthDate,
        PhoneNumber? phoneNumber,
        Address? address)
    {
        DomainException.When(fullName is null, "The fullname object cannot be null.");
        DomainException.When(email is null, "The email object cannot be null.");
        DomainException.When(birthDate is null, "The birth date object cannot be null.");
        DomainException.When(phoneNumber is null, "The phone number object cannot be null.");
        DomainException.When(address is null, "The address object cannot be null.");
    }

    public void Activate()
    {
        Status = EUserStatus.Active;

        AddEvent(new UserActivedEvent(Id, Status));
    }

    public void Deactivate()
    {
        Status = EUserStatus.Inactive;

        AddEvent(new UserDeactivedEvent(Id, Status));
    }

    public void AddSkills(params UserTechnology[] technologies)
    {
        DomainException.When(!technologies.Any(), "The skills cannot empty.");

        var skillsDescriptions = technologies.Select(s => s.TechnologyId);

        var skillAlreadyExists = Technologies.Any(s => skillsDescriptions.Contains(s.TechnologyId));

        DomainException.When(skillAlreadyExists, "The user already has one or more skills entered.");

        Technologies.AddRange(technologies);

        AddEvent(new UserAddedSkillsEvent(Id, Technologies));
    }

    public void UpdateDetails(
        FullName fullName,
        Email email,
        BirthDate birthDate,
        PhoneNumber phoneNumber,
        Address address)
    {
        ChangeFullName(fullName);
        ChangeEmail(email);
        ChangeBirthDate(birthDate);
        ChangePhoneNumber(phoneNumber);
        ChangeAddress(address);
    }

    public void ChangeFullName(FullName fullName)
    {
        FullName = fullName;

        AddEvent(new UserFullNameChangedEvent(Id, FullName));
    }

    public void ChangeEmail(Email email)
    {
        Email = email;

        AddEvent(new UserEmailChangedEvent(Id, Email));
    }

    public void ChangeBirthDate(BirthDate birthDate)
    {
        BirthDate = birthDate;

        AddEvent(new UserBirthDateChangedEvent(Id, BirthDate));
    }

    public void ChangePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;

        AddEvent(new UserPhoneNumberChangedEvent(Id, PhoneNumber));
    }

    public void ChangeAddress(Address address)
    {
        Address = address;

        AddEvent(new UserAddressChangedEvent(Id, Address));
    }
}