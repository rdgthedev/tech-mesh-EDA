namespace TechMesh.User.Domain.ValueObjects;

public class PhoneNumber
{
    public string Value { get; private set; }

    public PhoneNumber(string value)
    {
        DomainException.When(string.IsNullOrEmpty(value), "Phone number cannot be empty.");

        Value = value;
    }

    public static implicit operator PhoneNumber(string value) => new(value);
    public static implicit operator string(PhoneNumber value) => value.Value;

    public override bool Equals(object? obj)
    {
        return obj is PhoneNumber phoneNumber
               && phoneNumber.Value == Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}